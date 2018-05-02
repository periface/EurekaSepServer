using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Castle.Components.DictionaryAdapter;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students;
using Eureka.Spe.Students.Entities;
using Eureka.Spe.StudentsInfo.AcademicUnits.Dto;

namespace Eureka.Spe.StudentsInfo.AcademicUnits
{
    public class AcademicUnitAppService : IAcademicUnitAppService
    {
        private readonly IRepository<AcademicUnit> _repository;
        private readonly IAcademicUnitManager _academicUnitManager;
        public AcademicUnitAppService(IRepository<AcademicUnit> repository, IAcademicUnitManager academicUnitManager)
        {
            _repository = repository;
            _academicUnitManager = academicUnitManager;
        }

        public IQueryable<AcademicUnit> GetFilteredQuery(IQueryable<AcademicUnit> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Name.Contains(input.search));
            return all;
        }

        public IQueryable<AcademicUnit> GetOrderedQuery(IQueryable<AcademicUnit> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "name":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Name) : all.OrderBy(a => a.Name);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<AcademicUnitDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAll();

            var filtered = GetFilteredQuery(all, input);

            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();
            return new PagedResultDto<AcademicUnitDto>(filtered.Count(), paged.Select(a => a.MapTo<AcademicUnitDto>()).ToList());
        }

        public async Task<int> CreateOrUpdate(AcademicUnitDto input)
        {
            var elm = input.MapTo<AcademicUnit>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
            return elm.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<AcademicUnitDto> Get(int idValue)
        {
            var elm = await _repository.GetAsync(idValue);
            return elm.MapTo<AcademicUnitDto>();
        }

        public List<AcademicUnitDto> GetAcademicUnitSimpleList()
        {
            var all = _repository.GetAllList();
            return all.Select(a => a.MapTo<AcademicUnitDto>()).ToList();
        }

        public List<AcademicUnitSelectedDto> GetAcademicUnitSimpleListForEntity(string entityName, int id)
        {
            var result = new List<AcademicUnitSelectedDto>();
            var found = GetAcademicUnitsForEntity(entityName, id);
            var all = _repository.GetAllList().Select(a => a.MapTo<AcademicUnitDto>()).ToList();

            var academicUnitsNonSelected = all.Where(x=> found.All(a => a.Id != x.Id));

            result.AddRange(found.Select(a =>
            {
                var mapped = a.MapTo<AcademicUnitSelectedDto>();
                mapped.Selected = true;
                return mapped;
            }));
            result.AddRange(academicUnitsNonSelected.Select(a =>
            {
                var mapped = a.MapTo<AcademicUnitSelectedDto>();
                mapped.Selected = false;
                return mapped;
            }));
            return result;
        }
        

        public async Task AddAcademicUnitToEntity(AddToEntityInput input)
        {
            await _academicUnitManager.AddAcademicUnitsToEntity(input.EntityName, input.Id, input.Ids);
        }

        public List<AcademicUnitDto> GetAcademicUnitsForEntity(string entityName, int id)
        {
            switch (entityName)
            {
                case "feeds":
                    var academicUnitsForFeeds = _repository.GetAllIncluding(a => a.Feeds).Where(a => a.Feeds.Any(f => f.Id == id)).ToList();
                    return academicUnitsForFeeds.Select(a => a.MapTo<AcademicUnitDto>()).ToList();
                default:
                    return new EditableList<AcademicUnitDto>();
            }
        }
    }
}
