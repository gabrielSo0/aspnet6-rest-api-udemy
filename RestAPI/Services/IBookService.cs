using RestAPI.Data.VO;
using RestAPI.Model;

namespace RestAPI.Services
{
    public interface IBookService
    {
        BookVO Create(BookVO book);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
