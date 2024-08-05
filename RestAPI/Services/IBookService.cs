using RestAPI.Model;

namespace RestAPI.Services
{
    public interface IBookService
    {
        Book Create(Book person);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book person);
        void Delete(long id);
    }
}
