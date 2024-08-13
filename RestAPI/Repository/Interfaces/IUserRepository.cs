using RestAPI.Data.VO;
using RestAPI.Model;

namespace RestAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
    }
}
