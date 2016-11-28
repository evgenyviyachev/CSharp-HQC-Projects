namespace PhotoShare.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        //void Delete(TEntity entity);

        //void DeleteRange(IEnumerable<TEntity> entities);

        TEntity FindById(int id);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> where);
    }
}
