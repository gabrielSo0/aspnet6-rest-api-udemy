﻿using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI.Model;
using RestAPI.Model.Context;
using RestAPI.Repository.Interfaces;

namespace RestAPI.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
