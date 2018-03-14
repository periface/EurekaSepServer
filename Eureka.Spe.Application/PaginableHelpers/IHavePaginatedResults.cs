using System.Linq;
using Abp.Application.Services.Dto;

namespace Eureka.Spe.PaginableHelpers
{
    public interface IHavePaginatedResults<TEntity,TEntityDto,TPaginableInput>
    {
        IQueryable<TEntity> GetFilteredQuery(IQueryable<TEntity> all, TPaginableInput input);
        IQueryable<TEntity> GetOrderedQuery(IQueryable<TEntity> all, TPaginableInput input);
        PagedResultDto<TEntityDto> GetAll(TPaginableInput input);
    }
}
