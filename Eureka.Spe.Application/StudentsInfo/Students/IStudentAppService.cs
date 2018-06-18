using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.FileUpload;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Entities;
using Eureka.Spe.StudentsInfo.Students.Dto;

namespace Eureka.Spe.StudentsInfo.Students
{
    public interface IStudentAppService : IApplicationService,IHavePaginatedResults<Student,StudentDto,BootstrapTableInput>
    {
        Task SetPhone(PhoneInfoDto phone);

        Task SetFacebookToken(string token);

        Task<StudentDto> ConfirmInfo(CheckStudentInfoInput input);

        Task<List<PhoneInfoDto>> GetPhonesForStudent(int studentId);

        SaveFileOutput ChangeProfilePicture(ChangeProfileInput input);

        Task<StudentDto> Signup(SignupInput input);
    }
}
