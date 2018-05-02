using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.ContinuousEducation.CourseCategories.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.ContinuousEducation.CourseCategories
{
    public class CourseCategoriesAppService : ICourseCategoriesAppService
    {
        private readonly IRepository<CourseCategory> _repository;

        public CourseCategoriesAppService(IRepository<CourseCategory> repository)
        {
            _repository = repository;
        }

        public IQueryable<CourseCategory> GetFilteredQuery(IQueryable<CourseCategory> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Name.Contains(input.search));
            return all;
        }

        public IQueryable<CourseCategory> GetOrderedQuery(IQueryable<CourseCategory> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "name":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Name) : all.OrderBy(a => a.Name);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<CourseCategoryDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAll();

            var filtered = GetFilteredQuery(all,input);

            var ordered = GetOrderedQuery(filtered,input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();
            return  new PagedResultDto<CourseCategoryDto>(filtered.Count(),paged.Select(a=>a.MapTo<CourseCategoryDto>()).ToList());
        }

        public async Task<int> CreateOrUpdate(CourseCategoryDto input)
        {
            var elm = input.MapTo<CourseCategory>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
            return elm.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<CourseCategoryDto> Get(int id)
        {
            var elm = await _repository.GetAsync(id);
            return elm.MapTo<CourseCategoryDto>();
        }

        public List<CourseCategoryDto> GetCourseList()
        {
            var courses = _repository.GetAllList();
            return courses.Select(a => a.MapTo<CourseCategoryDto>()).ToList();
        }
    }
}
