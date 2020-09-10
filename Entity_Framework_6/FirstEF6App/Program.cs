// DbContext: определяет контекст данных, используемый для взаимодействия с базой данных.
// DbModelBuilder: сопоставляет классы на языке C# с сущностями в базе данных.
// DbSet/DbSet<TEntity>: представляет набор сущностей, хранящихся в базе данных

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEF6App
{
    class Program
    {
        /*
        В любом приложении, работающим с БД через Entity Framework, нам нужен будет контекст (класс производный от DbContext) 
        и набор данных DbSet, через который мы сможем взаимодействовать с таблицами из БД. В данном случае таким контекстом 
        является класс UserContext.
        */
        class UserContext : DbContext
        {
            /*
            В конструкторе этого класса вызывается конструктор базового класса, в который передается строка 
            "DbConnection" - это имя будущей строки подключения к базе данных. В принципе мы можем не использовать 
            конструктор, тогда в этом случае строка подключения носила бы имя самого класса контекста данных.
            */
            public UserContext() : base("DbConnection")
            { 
            }
            /*
            Свойство Users хранить набор объектов User. В классе контекста данных набор объектов представляет класс DbSet<T>. 
            Через это свойство будет осуществляться связь с таблицей объектов User в бд.
            */
            public DbSet<User> Users { get; set; }
        }

        public class User
        { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        }

        static void Main(string[] args)
        {
            using (UserContext db = new UserContext())
            {
                // создаем два объекта User
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Sam", Age = 26 };

                // добавляем их в бд
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.Users;
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name, u.Age);
                }
            }
            Console.Read();



        }
    }
}
