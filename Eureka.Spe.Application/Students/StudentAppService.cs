using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Dto;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Students
{
    public class StudentAppService : IStudentAppService
    {
        private readonly IRepository<Student> _repository;

        public StudentAppService(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public IQueryable<Student> GetFilteredQuery(IQueryable<Student> all, BootstrapTableInput input)
        {
            all = all.WhereIf(!string.IsNullOrEmpty(input.search),
                a => a.Name.Contains(input.search) || a.Surname.Contains(input.search));
            return all;
        }

        public IQueryable<Student> GetOrderedQuery(IQueryable<Student> all, BootstrapTableInput input)
        {
            switch (input.sort)
            {
                case "name":
                    return input.order == "desc" ? all.OrderByDescending(a => a.Name) : all.OrderBy(a => a.Name);
                default:
                    return input.order == "desc" ? all.OrderByDescending(a => a.CreationTime) : all.OrderBy(a => a.CreationTime);
            }
        }

        public PagedResultDto<StudentDto> GetAll(BootstrapTableInput input)
        {
            var all = _repository.GetAllIncluding(a => a.PhoneInfos);

            var filtered = GetFilteredQuery(all, input);

            var ordered = GetOrderedQuery(filtered, input);

            var paginated = ordered.Skip(input.offset).Take(input.limit).ToList();

            return new PagedResultDto<StudentDto>(filtered.Count(), paginated.Select(a => a.MapTo<StudentDto>()).ToList());
        }

        public async Task CreateOrUpdate(StudentDto input)
        {
            var mapped = input.MapTo<Student>();
            await _repository.InsertOrUpdateAndGetIdAsync(mapped);
        }

        public async Task<StudentDto> Get(int id)
        {
            var student = await _repository.GetAsync(id);
            return student.MapTo<StudentDto>();
        }
        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public Task SetPhone(string token)
        {
            throw new NotImplementedException();
        }

        public Task SetFacebookToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmInfo(CheckStudentInfoInput input)
        {
            throw new NotImplementedException();
        }
    }
}
