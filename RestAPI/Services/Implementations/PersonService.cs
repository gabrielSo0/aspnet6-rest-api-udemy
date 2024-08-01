using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI.Model;
using RestAPI.Model.Context;

namespace RestAPI.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private MySQLContext _context;
        public PersonService(MySQLContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return person;
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if(result != null)
            {
                _context.Persons.Remove(result);
                _context.SaveChanges();
            }
        }

        private bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
