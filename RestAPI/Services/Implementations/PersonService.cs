using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI.Data.Converter;
using RestAPI.Data.VO;
using RestAPI.Model;
using RestAPI.Model.Context;
using RestAPI.Repository.Generic;
using RestAPI.Repository.Interfaces;

namespace RestAPI.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;
        public PersonService(IPersonRepository repository)
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

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
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

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);

            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
