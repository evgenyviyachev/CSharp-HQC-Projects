namespace ACTester.Data.Repositories
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> set;

        public Repository(DbSet<TEntity> set)
        {
            this.set = set;
        }

        public void Add(TEntity entity)
        {
            this.set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.set.AddRange(entities);
        }

        public int Count()
        {
            return this.set.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return this.set.Where(where.Compile()).Count();
        }

        public TEntity Find(int id)
        {
            return this.set.Find(id);
        }

        public TEntity First()
        {
            return this.set.FirstOrDefault();
        }

        public TEntity First(Expression<Func<TEntity, bool>> where)
        {
            return this.set.Where(where.Compile()).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.set;
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where)
        {
            return this.set.Where(where.Compile());
        }

        public void Remove(TEntity entity)
        {
            this.set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.set.RemoveRange(entities);
        }
    }
}
