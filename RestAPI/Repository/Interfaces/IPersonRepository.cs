using RestAPI.Model;
using RestAPI.Repository.Generic;

namespace RestAPI.Repository.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
