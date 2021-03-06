﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Eureka.Spe.FileUpload;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Entities;
using Eureka.Spe.StudentsInfo.Students.Dto;
using Microsoft.AspNet.Identity;

namespace Eureka.Spe.StudentsInfo.Students
{
    public class StudentAppService : IStudentAppService
    {
        private readonly IRepository<Student> _repository;
        private readonly IRepository<AcademicUnit> _academicUnitRepository;
        private readonly IRepository<PhoneInfo> _phoneInfoRepository;
        
        public StudentAppService(IRepository<Student> repository, IRepository<AcademicUnit> academicUnitRepository, IRepository<PhoneInfo> phoneInfoRepository)
        {
            _repository = repository;
            _academicUnitRepository = academicUnitRepository;
            _phoneInfoRepository = phoneInfoRepository;
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

        public async Task<int> CreateOrUpdate(StudentDto input)
        {
            var mapped = input.MapTo<Student>();
            if (input.Id != 0)
            {
                var existing = _repository.Get(input.Id);
                var pass = existing.Password;
                var entity = input.MapTo(existing);

                entity.Password = pass;

                await _repository.UpdateAsync(entity);
                return entity.Id;
            }
            mapped.Password = new PasswordHasher().HashPassword(input.Password);
            await _repository.InsertOrUpdateAndGetIdAsync(mapped);
            return mapped.Id;
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
        /// <summary>
        /// Saves the phone without student info
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task SetPhone(PhoneInfoDto phone)
        {
            var phoneCoincidence = _phoneInfoRepository.FirstOrDefault(a => a.Token == phone.Token);
            if (phoneCoincidence != null) return;
            var mapped = phone.MapTo<PhoneInfo>();
            await _phoneInfoRepository.InsertAndGetIdAsync(mapped);
        }
        public Task SetFacebookToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentDto> ConfirmInfo(CheckStudentInfoInput input)
        {
            var student = await _repository.GetAllIncluding(a => a.Career).FirstOrDefaultAsync(a => a.EnrollCode == input.EnrollCode);
            if (student == null) return new StudentDto();
            var uncrypt = new PasswordHasher().VerifyHashedPassword(student.Password, input.Password);
            if (uncrypt == PasswordVerificationResult.Success)
            {
                var phone = _phoneInfoRepository.FirstOrDefault(a => a.Token == input.PhoneToken);
                if (phone != null)
                {
                    phone.StudentId = student.Id;
                    await _phoneInfoRepository.UpdateAsync(phone);
                }
                else
                {
                    if (!string.IsNullOrEmpty(input.PhoneToken))
                    {
                        var newPhone = new PhoneInfo()
                        {
                            Token = input.PhoneToken,
                            StudentId = student.Id
                        };
                        await _phoneInfoRepository.InsertOrUpdateAndGetIdAsync(newPhone);
                    }
                    
                }
            }
            return uncrypt == PasswordVerificationResult.Success ? MapStudent(student) : new StudentDto();
        }

        private StudentDto MapStudent(Student student)
        {
            var mapped = student.MapTo<StudentDto>();

            mapped.AcademicInfo = new AdacemicInfoDto
            {
                Career = student.Career.Name,
                CareerId = student.Career.Id
            };


            var academicUnit = _academicUnitRepository.Get(student.Career.AcademicUnitId);

            mapped.AcademicInfo.AcademicUnit = academicUnit.ShortName;
            mapped.AcademicInfo.AcademicUnitId = academicUnit.Id;

            return mapped;
        }

        public async Task<List<PhoneInfoDto>> GetPhonesForStudent(int studentId)
        {
            var student = await Task.FromResult(_repository.GetAllIncluding(a => a.PhoneInfos).FirstOrDefault(a => a.Id == studentId));
            return student.PhoneInfos.Select(a => a.MapTo<PhoneInfoDto>()).ToList();
        }

        public SaveFileOutput ChangeProfilePicture(ChangeProfileInput input)
        {
            var result = FileSaver.SaveFileFromBase64String(input.Base64Img,
                input.StudentId.ToString(),
                "students",
                "img",
                HostingEnvironment.ApplicationPhysicalPath,
                HttpContext.Current);

            var student = _repository.Get(input.StudentId);

            student.Img = result.FileUrl;

            _repository.Update(student);

            return result;
        }

        public Task<StudentDto> Signup(SignupInput input)
        {
            throw new NotImplementedException();
        }
        public async Task<StudentDto> RegisterStudentAccount(StudentDto input)
        {
            var passwordHash = new PasswordHasher().HashPassword(input.Password);
            input.Password = passwordHash;
            var mapped = input.MapTo<Student>();
            await _repository.InsertOrUpdateAndGetIdAsync(mapped);
            return mapped.MapTo<StudentDto>();
        }
    }
}
