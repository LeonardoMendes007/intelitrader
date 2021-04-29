
using System.Collections.Generic;
using ApiUser.Models;

namespace ApiUser.Services{

    public interface IUserService{

        List<User> findAll();
        User findById(int id);
        void save(User user);
        void delete(User user);    
        void update(User user);
        bool saveChanges();
        
        
    }
}