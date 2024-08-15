using RestAPI.Data.VO;
using RestAPI.Model;
using RestAPI.Model.Context;
using RestAPI.Repository.Interfaces;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RestAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;
        public UserRepository(MySQLContext context)
        {
            _context = context;
        }
        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => 
                            (u.UserName == user.UserName) && (u.Password == pass));   
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u =>
                            u.UserName == userName);
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u =>
                            u.UserName == userName);

            if (user is null) return false;

            user.RefreshToken = null;
            _context.SaveChanges();
            
            return true;    
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }

        private object ComputeHash(string password, HashAlgorithm hashAlgorithm)
        {
            Byte[] hashedBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            // StringBuilder is more fast because it make use of the buffer already created and can substitute the common string concatenation
            var sBuilder = new StringBuilder();

            foreach(var item in hashedBytes)
            {
                // item.ToString("x2") transform the byte array into hexadecimal format
                // ex: instead of 13, will be 0D
                sBuilder.Append(item.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
