namespace PhotoShare.Data.Database
{
    using Contracts;
    using Server;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<TEntity>
        : IRepository<TEntity> where TEntity : class
    {
        protected readonly PhotoShareContext context;

        public Repository(PhotoShareContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.context.Set<TEntity>().AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            this.context.Set<TEntity>().RemoveRange(entities);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> where)
        {
            return this.context.Set<TEntity>().Where(where);
        }

        public TEntity FindById(int id)
        {
            return this.context.Set<TEntity>().Find(id);
        }
    }
}
