using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Eureka.Spe.EntityFramework.Repositories
{
    public abstract class SpeRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SpeDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected SpeRepositoryBase(IDbContextProvider<SpeDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class SpeRepositoryBase<TEntity> : SpeRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected SpeRepositoryBase(IDbContextProvider<SpeDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
