using System.Collections;

namespace Assignment4.Orm
{
    public interface IParser
    {
        Task ParseAndInsert(object obj);
        Task ParseAndDelete(object obj);
        Task ParseAndDeleteById(object id, Type type);
        Task ParseAndUpdate(object obj);
        Task<dynamic> ParseAndGetById<G>(G? id, Type type);
        Task<IList> ParseAndGetAll(Type type);
    }
}