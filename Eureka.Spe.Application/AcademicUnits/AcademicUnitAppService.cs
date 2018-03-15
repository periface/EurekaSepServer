using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.AcademicUnits.Dto;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.AcademicUnits
{
    public class AcademicUnitAppService : IAcademicUnitAppService
    {
        private readonly IRepository<AcademicUnit> _repository;

        public AcademicUnitAppService(IRepository<AcademicUnit> repository)
        {
            _repository = repository;
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

        public async Task CreateOrUpdate(AcademicUnitDto input)
        {
            var elm = input.MapTo<AcademicUnit>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
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
    }
}
