using System.Collections;

namespace Assignment4.Orm
{
    public interface IMyORM<G, T>
        where T : class, IEntity<G>
    {
        Task Delete(G id);
        Task Delete(T item);
        Task<IList> GetAll();
        Task<dynamic> GetById(G id);
        Task Insert(T item);
        Task Update(T item);
    }
}