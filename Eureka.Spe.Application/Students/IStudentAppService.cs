using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Dto;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Students
{
    public interface IStudentAppService : IApplicationService,IHavePaginatedResults<Student,StudentDto,BootstrapTableInput>
    {
        Task SetPhone(PhoneInfoDto phone);

        Task SetFacebookToken(string token);

        Task<StudentDto> ConfirmInfo(CheckStudentInfoInput input);

        Task<List<PhoneInfoDto>> GetPhonesForStudent(int studentId);


    }
}
