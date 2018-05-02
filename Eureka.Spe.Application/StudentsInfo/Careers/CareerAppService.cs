using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.Students.Entities;
using Eureka.Spe.StudentsInfo.Careers.Dto;

namespace Eureka.Spe.StudentsInfo.Careers
{
    public class CareerAppService : ICareerAppService
    {
        private readonly IRepository<Career> _repository;
        private readonly IRepository<AcademicUnit> _academicUnitsRepository;
        public CareerAppService(IRepository<Career> repository, IRepository<AcademicUnit> academicUnitsRepository)
        {
            _repository = repository;
            _academicUnitsRepository = academicUnitsRepository;
        }

        public IQueryable<Career> GetFilteredQuery(IQueryable<Career> all, CustomCareerInput input)
        {
            if (input.AcademicUnitId != 0) all = all.Where(a => a.AcademicUnitId == input.AcademicUnitId);
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Name.Contains(input.search));
            return all;
        }

        public IQueryable<Career> GetOrderedQuery(IQueryable<Career> all, CustomCareerInput input)
        {
            switch (input.sort)
            {
                case "name":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Name) : all.OrderBy(a => a.Name);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }
        public PagedResultDto<CareerDto> GetAll(CustomCareerInput input)
        {
            var all = _repository.GetAll();

            var filtered = GetFilteredQuery(all, input);

            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();
            return new PagedResultDto<CareerDto>(filtered.Count(), paged.Select(a => a.MapTo<CareerDto>()).ToList());
        }

        public async Task<int> CreateOrUpdate(CareerDto input)
        {
            var elm = input.MapTo<Career>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
            return elm.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<CareerDto> Get(int id)
        {
            var found = await _repository.GetAsync(id);
            return found.MapTo<CareerDto>();
        }

        public List<CareerDto> GetCareersSimpleList()
        {
            var all = _repository.GetAllList();
            return all.Select(a => a.MapTo<CareerDto>()).ToList();
        }

        public CareersGroupedList GetCareersList()
        {
            var result = new CareersGroupedList();
            var allAcademicUnits = _academicUnitsRepository.GetAllIncluding(a=>a.Careers).ToList();
            foreach (var allAcademicUnit in allAcademicUnits)
            {
                var group = new Group()
                {
                    Name = allAcademicUnit.Name
                };
                foreach (var career in allAcademicUnit.Careers)
                {
                    group.Careers.Add(career.MapTo<CareerDto>());
                }
                result.Groups.Add(group);
            }
            return result;
        }

        public List<CareerDto> GetCareersSimpleListForAcUnit(int id)
        {
            var careers = _repository.GetAllList(a => a.AcademicUnitId == id);
            return careers.Select(a => a.MapTo<CareerDto>()).ToList();
        }
    }
}
