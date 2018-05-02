using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.ContinuousEducation.Courses.Dto;
using Eureka.Spe.Courses.Entities;
using Eureka.Spe.PaginableHelpers;

namespace Eureka.Spe.ContinuousEducation.Courses
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


            mapped.RegistrationsOpen = course.RegistrationsStart.HasValue &&
                                       course.RegistrationsStart.Value<= DateTime.Now 
                                       && course.RegistrationsEnd.HasValue 
                                       && course.RegistrationsEnd >= DateTime.Now;

            if (course.EndDate != null)
                if (course.StartDate != null)
                    mapped.Duration =  $"Duración: de {course.StartDate.Value.ToShortDateString()} al {course.EndDate.Value.ToShortDateString()}";
            return mapped;
        }
        public async Task<int> CreateOrUpdate(CourseDto input)
        {
            var elm = input.MapTo<Course>();
            await _repository.InsertOrUpdateAndGetIdAsync(elm);
            return elm.Id;
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<CourseDto> Get(int id)
        {
            var course = await _repository.GetAsync(id);
            return Map(course);
        }
    }
}
