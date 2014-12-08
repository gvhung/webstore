using System.Collections.Generic;
using Entities;

namespace BuisnessLogicLayer
{
    public interface IBuisnessLogicLayer<TEntity, TId> where TEntity : BaseEntity
    {
        TEntity Read(TId Id);
        IEnumerable<TEntity> ReadAll();
        void Update(TEntity entity);
        void Delete(TId Id);
        void Create(TEntity entity);
    }
}
