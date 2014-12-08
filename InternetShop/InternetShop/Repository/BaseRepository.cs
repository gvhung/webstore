using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entities;

namespace Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity, int> where TEntity : BaseEntity
    {
        public void Create(TEntity entity)
        {
            using (var context = new ShopContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public virtual TEntity Read(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            }
        }

        public virtual void Update(TEntity entity)
        {
            Delete(entity.Id);
            Create(entity);
        }

        public void Delete(int id)
        {
            using (var context = new ShopContext())   
            {
                context.Set<TEntity>().Remove
                    (context.Set<TEntity>().FirstOrDefault(e => e.Id == id));
                context.SaveChanges();
            }
        }

        public virtual IEnumerable<TEntity> ReadAll()
        {
            using (var context = new ShopContext())
            {
                //return context.Set<TEntity>();
                return context.Set<TEntity>().ToListAsync().Result;
                
            }
        }
    }
}
