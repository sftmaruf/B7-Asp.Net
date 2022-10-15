namespace Assignment4
{
    public interface IMyORM<G, T>
        where T : class, IEntity
    {
        void Delete(G id);
        void Delete(T item);
        void GetAll(G id);
        void GetById(G id);
        void Insert(T item);
        void Update(T item);
    }
}