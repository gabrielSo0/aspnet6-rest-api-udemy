using RestAPI.Data.VO;

namespace RestAPI.Services
{
    public interface IPersonService
    {
        PersonVO Create(PersonVO personVO);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO personVO);
        void Delete(long id);
    }
}
