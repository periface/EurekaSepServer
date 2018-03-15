using System.Threading.Tasks;
using Abp.Application.Services;
using Eureka.Spe.PaginableHelpers;
using Eureka.Spe.Students.Dto;
using Eureka.Spe.Students.Entities;

namespace Eureka.Spe.Students
{
    public interface IStudentAppService : IApplicationService,IHavePaginatedResults<Student,StudentDto,BootstrapTableInput>
    {
        Task SetPhone(string token);

        Task SetFacebookToken(string token);

        Task ConfirmInfo(CheckStudentInfoInput input);

        Task<StudentDto> Get(int id);
    }
}
