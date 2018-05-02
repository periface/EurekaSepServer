using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Scholarships.Entities;
using Eureka.Spe.ScholarshipsService.Scholarships.Dto;

namespace Eureka.Spe.ScholarshipsService.Scholarships
{
    public class ScholarshipAppService : IScholarshipAppService
    {
        private readonly IRepository<Scholarship> _repository;

        public ScholarshipAppService(IRepository<Scholarship> repository)
        {
            _repository = repository;
        }

        public IQueryable<Scholarship> GetFilteredQuery(IQueryable<Scholarship> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Title.Contains(input.search));
            return all;
        }

        public IQueryable<Scholarship> GetOrderedQuery(IQueryable<Scholarship> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "title":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Title) : all.OrderBy(a => a.Title);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<ScholarshipDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAllIncluding(a => a.ScholarshipSections);

            var filtered = GetFilteredQuery(all, input);
            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();

            return new PagedResultDto<ScholarshipDto>(filtered.Count(), BuildModel(paged));
        }

        private IReadOnlyList<ScholarshipDto> BuildModel(List<Scholarship> paged)
        {
            return paged.Select(a =>
            {
                var mapped = a.MapTo<ScholarshipDto>();
                mapped.Sections = a.ScholarshipSections?.Count;
                return mapped;
            }).ToList();
        }

        public async Task<int> CreateOrUpdate(ScholarshipDto input)
        {
            var mapped = input.MapTo<Scholarship>();
            await _repository.InsertOrUpdateAndGetIdAsync(mapped);
            return mapped.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ScholarshipDto> Get(int id)
        {
            var elm = await _repository.GetAsync(id);
            return elm.MapTo<ScholarshipDto>();
        }
    }
}
