namespace PhotoShare.Test.Setup
{
    using Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class CustomRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private List<TEntity> list;

        public CustomRepository()
        {
            this.list = new List<TEntity>();
        }

        public void Add(TEntity entity)
        {
            this.list.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.list.AddRange(entities);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> where)
        {
            return this.list.FindAll(where.Compile().Invoke);
        }

        public TEntity FindById(int id)
        {
            if (id > this.list.Count)
            {
                return null;
            }

            return this.list[id - 1];
        }
    }
}
