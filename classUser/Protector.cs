using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace classUser
{
   public class Protector
   {
        public static Dictionary<string, User> Users = new Dictionary<string, User>();
        public static User Register(string username, string password)
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            var saltedhashedPassword = SaltAndHashPassword(password, saltText);
            var user = new User
            {
                Name = username,
                Salt = saltText,
                saltedHashedPassword = saltedhashedPassword
            };
            Users.Add(user.Name, user);
            return user;
        }
        public static string SaltAndHashPassword(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }
        public static bool CheckPassword(string username, string password)
        {
            if(!Users.ContainsKey(username))
            {
                return false;
            }
            var user = Users[username];

            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);
            return (saltedhashedPassword == user.saltedHashedPassword);
        }
       


    }
}
