using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Eureka.Spe.Scholarships.Entities;
using Eureka.Spe.ScholarshipsService.ScholarshipSections.Dto;

namespace Eureka.Spe.ScholarshipsService.ScholarshipSections
{
    public class ScholarshipSectionAppService: IScholarshipSectionAppService
    {
        private readonly IRepository<ScholarshipSection> _repository;

        public ScholarshipSectionAppService(IRepository<ScholarshipSection> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrUpdate(ScholarshipSectionDto input)
        {
            var mapped = input.MapTo<ScholarshipSection>();
            await _repository.InsertOrUpdateAndGetIdAsync(mapped);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ScholarshipSectionDto> Get(int id)
        {
            var elm = await _repository.GetAsync(id);
            return elm.MapTo<ScholarshipSectionDto>();
        }

        public async Task<List<ScholarshipSectionDto>> GetSections(int scholarshipId)
        {
            var sections = await _repository.GetAllListAsync(a => a.ScholarshipId == scholarshipId);
            return sections.Select(a => a.MapTo<ScholarshipSectionDto>()).ToList();
        }
    }
}
