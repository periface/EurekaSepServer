using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services.Dto;

namespace Eureka.Spe.PaginableHelpers
{
    public interface IHavePaginatedResults<TEntity,TEntityDto,TPaginableInput>
    {
        [ApiIgnore]
        IQueryable<TEntity> GetFilteredQuery(IQueryable<TEntity> all, TPaginableInput input);
        [ApiIgnore]
        IQueryable<TEntity> GetOrderedQuery(IQueryable<TEntity> all, TPaginableInput input);
        PagedResultDto<TEntityDto> GetAll(TPaginableInput input);


        Task<int> CreateOrUpdate(TEntityDto input);
        Task Delete(int id);
        [HttpGet]
        Task<TEntityDto> Get(int id);
    }
}
