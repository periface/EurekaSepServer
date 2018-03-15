using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.Careers.Dto;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Careers
{
    public class CareerAppService : ICareerAppService
    {
        private readonly IRepository<Career> _repository;

        public CareerAppService(IRepository<Career> repository)
        {
            _repository = repository;
        }

        public IQueryable<Career> GetFilteredQuery(IQueryable<Career> all, BootstrapTableInput input)
        {
            all = all.WhereIf(string.IsNullOrEmpty(input.search), a => a.Name.Contains(input.search));
            return all;
        }

        public IQueryable<Career> GetOrderedQuery(IQueryable<Career> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "name":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Name) : all.OrderBy(a => a.Name);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<CareerDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAll();

            var filtered = GetFilteredQuery(all, input);

            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();
            return new PagedResultDto<CareerDto>(filtered.Count(), paged.Select(a => a.MapTo<CareerDto>()).ToList());
        }

        public async Task CreateOrUpdate(CareerDto input)
        {
            var elm = input.MapTo<Career>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public Task<CareerDto> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
