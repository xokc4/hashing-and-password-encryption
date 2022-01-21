using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace LessonOne_IS_20_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("напиши  нуль, шифровка пароля - 2, шифровка слова - 1");
            int a = Convert.ToInt32(Console.ReadLine());
            if(a ==0)
            {
                FilePassword();
            }
            if (a == 1)
            {
                hifrMessage();
            }
            if (a == 2)
            {
                hifrPassword();
            }
        }
        /// <summary>
        /// шифровка паролей
        /// </summary>
        public static void hifrMessage()
        {
            while (true)
            {
                Console.WriteLine("Введите сообщение");
                var conntent = Console.ReadLine();
                Console.WriteLine("Введите пароль для шифрования");
                var password = Console.ReadLine();
                var contentAES = classUser.ProtectorPasswordUser.Encrypt(conntent, password);
                Console.WriteLine("Результат расшифровки " + classUser.ProtectorPasswordUser.Decrypt(contentAES, password));
                Console.WriteLine($"{contentAES}");
            }
        }
        /// <summary>
        /// хеширование для внутренних пользователей
        /// </summary>
        public static void hifrPassword()
        {
            while(true)
            {
                Console.WriteLine("Создание нового пользователя");
                Console.WriteLine("укажите имя пользователя");
                var name = Console.ReadLine();
                Console.WriteLine("укажите пароль пользователя ");
                var password = Console.ReadLine();
                classUser.User user = classUser.Protector.Register(name, password);
                Console.WriteLine($" пользователь создан: \n"+
                    $" имя - {user.Name} " + $" соль - {user.Salt} " + $" пароль с солью - {user.saltedHashedPassword} ");
                Console.WriteLine(" Проверим работу ");
                Console.WriteLine(" укажите имя пользователя ");
                var name1 = Console.ReadLine();
                Console.WriteLine(" укажите пароль пользователя ");
                var password1 = Console.ReadLine();
                Console.WriteLine(" Пароль : " + classUser.Protector.CheckPassword(name1, password1));
            }
        }
        /// <summary>
        /// хеширование из файла
        /// </summary>
        public static void FilePassword()
        {
            
            classUser.JsonFile.FileReading();
            classUser.JsonFile.fileReadMap();
            classUser.JsonFile.fileNew();
        }
    }    
}
