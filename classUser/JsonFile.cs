using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace classUser
{
   public  class JsonFile
   {
        private static string path = @"C:\Users\user\Desktop\Новая папка\contentCard.json";
        private static string pathNew = @"C:\Users\user\Desktop\Новая папка\Новый текстовый документ.Json";
        public static List<UserJS> users1 = new List<UserJS>();
        public static void  FileReading()
        {
            List<UserJS> users = new List<UserJS>();
            string json = File.ReadAllText(path);
            users = JsonConvert.DeserializeObject<List<UserJS>>(json);
            List<UserJS> usersRedlain = new List<UserJS>();
            foreach (var us in users)
            {
                string map = us.Creditcard;

                string password = classUser.ProtectorPasswordUser.Encrypt(us.Password, classUser.ProtectorPasswordUser.PasswordUsersFile.ToString());

                string ConvertjsonMap = Protector.SaltAndHashPassword(password, map);

                Console.WriteLine(ConvertjsonMap);

                usersRedlain.Add(new UserJS(us.Name, ConvertjsonMap, password));
            }
            users1 = usersRedlain;
        }
        public static void fileReadMap()
        {
            string json = JsonConvert.SerializeObject(users1);// создания сериализации
            File.WriteAllText(pathNew, json);// запись
        }
        public static void fileNew()
        {
            List<UserJS> users = new List<UserJS>();
            string json = File.ReadAllText(pathNew);
            users = JsonConvert.DeserializeObject<List<UserJS>>(json);
            List<UserJS> usersRedlain = new List<UserJS>();
            
            foreach (var item in users)
            {
                string password = classUser.ProtectorPasswordUser.Decrypt(item.Password, classUser.ProtectorPasswordUser.PasswordUsersFile.ToString());
                var mapCredit = classUser.Protector.SaltAndHashPassword(password, item.Creditcard);
                Console.WriteLine("напишите номер кредитной карты");
                string mapCreditUser = Console.ReadLine();
                var map = classUser.Protector.SaltAndHashPassword(item.Password, mapCreditUser);
                if (item.Creditcard == map)
                {
                    Console.WriteLine("да");
                }
                if(item.Creditcard == mapCredit)
                {
                    Console.WriteLine("да");
                }
                else
                {
                    Console.WriteLine("нет");
                }
                

            }
            foreach(var item in usersRedlain)
            {
                Console.WriteLine($"имя: {item.Name}, карта: {item.Creditcard}, пароль: {item.Password}");
            }
        }

        public class UserJS
        {
          public   string   Name { get; set; }
            public string   Creditcard { get; set; }
            public  string   Password { get; set; }
            public UserJS(string name, string salt, string SaltedHashedPassword)
            {
                this.Name = name;
                this.Creditcard = salt;
                this.Password = SaltedHashedPassword;
            }
        }
       
   }
}
