﻿using RestAPI.Data.VO;
using RestAPI.Model;
using RestAPI.Model.Context;
using RestAPI.Repository.Interfaces;
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
