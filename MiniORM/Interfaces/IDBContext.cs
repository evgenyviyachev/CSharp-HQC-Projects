namespace MiniORM.Interfaces
{
    using System.Collections.Generic;

    public interface IDBContext
    {
        bool Persist(object entity);
        T FindByID<T>(int id);
        IEnumerable<T> FindAll<T>();
        IEnumerable<T> FindAll<T>(string where);
        T FindFirst<T>();
        T FindFirst<T>(string where);
        int DeleteFrom<T>();
        int DeleteFrom<T>(string where);
    }
}
