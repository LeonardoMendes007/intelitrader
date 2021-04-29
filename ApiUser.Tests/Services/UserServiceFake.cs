using System;
using System.Collections.Generic;
using System.Linq;
using ApiUser.Models;
using ApiUser.Repository;

namespace ApiUser.Services
{

    public class UserServiceFake : IUserService
    {
        List<User> users = new List<User>();

        public UserServiceFake()
        {
            users.Add(new User(1, "Leonardo", "Mendes", 23, DateTime.Now));
            users.Add(new User(2, "Ivana", "Maria", 59, DateTime.Now));
            users.Add(new User(3, "Jéssica", "Mendes", 30, DateTime.Now));

        }
        public List<User> findAll()
        {
            return users;
        }

        public User findById(int id)
        {
            return users.Find(x => x.Id == id);
        }

        public void save(User user)
        {
            
                int count = users.Last().Id;
                user.Id = count + 1;
                user.CreationDate = DateTime.Now;
                users.Add(user);
        }

        public void update(User user)
        {

        }

        public void delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            users.Remove(user);
        }

        public bool saveChanges()
        {
            throw new NotImplementedException();
        }
    }
}