using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.Courses.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.Courses
{
    public class CourseAppService : ICourseAppService
    {
        private readonly IRepository<Course> _repository;

        public CourseAppService(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public IQueryable<Course> GetFilteredQuery(IQueryable<Course> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search), a => a.Title.Contains(input.search));
            return all;
        }

        public IQueryable<Course> GetOrderedQuery(IQueryable<Course> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "name":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Title) : all.OrderBy(a => a.Title);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<CourseDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAllIncluding(a=>a.CourseCategory);

            var filtered = GetFilteredQuery(all, input);

            var ordered = GetOrderedQuery(filtered, input);

            var paged = ordered.Skip(input.offset).Take(input.limit).ToList();
            return new PagedResultDto<CourseDto>(filtered.Count(), paged.Select(Map).ToList());
        }

        public CourseDto Map(Course course)
        {
            var mapped = course.MapTo<CourseDto>();
            mapped.CategoryName = course.CourseCategory?.Name;
            return mapped;
        }
        public async Task CreateOrUpdate(CourseDto input)
        {
            var elm = input.MapTo<Course>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<CourseDto> Get(int id)
        {
            var course = await _repository.GetAsync(id);
            return course.MapTo<CourseDto>();
        }
    }
}
