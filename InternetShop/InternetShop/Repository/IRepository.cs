using System.Collections.Generic;
using Entities;

namespace Repository
{
    public interface IRepository<TEntity, TId> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> ReadAll();
        void Update(TEntity entity);
        void Delete(TId Id);
    }
}
