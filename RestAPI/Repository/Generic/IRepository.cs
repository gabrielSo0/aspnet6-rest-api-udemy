using RestAPI.Model;

namespace RestAPI.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T FindById(long id);
        List<T> FindAll();
        T Update(T person);
        void Delete(long id);
        bool Exists(long id);
    }
}
