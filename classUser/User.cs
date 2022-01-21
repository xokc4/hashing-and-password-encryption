using System;

namespace classUser
{
    public class User
    {
        public string Name { get; set; }
        public string Salt { get; set; }
        public string saltedHashedPassword { get; set; }
        public User(string name, string salt, string SaltedHashedPassword)
        {
            this.Name = name;
                this.Salt = salt;
            this.saltedHashedPassword = SaltedHashedPassword;
        }
        public User()
        {

        }
        public new string ToString()
        {
            return $"имя: {Name}, карта: {Salt}, пароль: {saltedHashedPassword}";
        }
    }
}
