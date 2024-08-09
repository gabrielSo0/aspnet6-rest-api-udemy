using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI.Data.Converter;
using RestAPI.Data.VO;
using RestAPI.Model;
using RestAPI.Model.Context;
using RestAPI.Repository.Generic;

namespace RestAPI.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;
        public PersonService(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);

            return _converter.Parse(_repository.Create(personEntity));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);

            return _converter.Parse(_repository.Update(personEntity));
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
