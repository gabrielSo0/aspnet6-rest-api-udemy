using RestAPI.Model;
using RestAPI.Model.Context;
using RestAPI.Repository.Interfaces;

namespace RestAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly MySQLContext _context;

        public BookRepository(MySQLContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(b => b.Id.Equals(id));
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return book;
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;

            var result = _context.Books.SingleOrDefault(b => b.Id.Equals(book.Id));

            if(result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return book;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(b => b.Id.Equals(id));

            if(result != null)
            {
                _context.Books.Remove(result);
                _context.SaveChanges();
            }
        }

        public bool Exists(long id)
        {
            return _context.Books.Any(b => b.Id.Equals(id));
        }
    }
}
