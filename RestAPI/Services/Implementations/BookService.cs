using RestAPI.Data.Converter;
using RestAPI.Data.VO;
using RestAPI.Model;
using RestAPI.Repository.Generic;

namespace RestAPI.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;
        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            return _converter.Parse(_repository.Create(bookEntity));
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            return _converter.Parse(_repository.Update(bookEntity));
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
