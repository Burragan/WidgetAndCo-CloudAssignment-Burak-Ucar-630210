using WidgetAndCo.Domain;
using WidgetAndCo.Domain.DTO;
using WidgetAndCo.Repository.Interface;
using WidgetAndCo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Service.Service
{
    public class UserService : IUserService
    {
        private IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            return UserRepository.Add(user);
        }

        public void DeleteUser(User user)
        {
           UserRepository.Delete(user);
        }

        public List<User> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }

        public User GetUser(int UserId)
        {
            return UserRepository.GetSingle(UserId);
        }

        public User UpdateUser(User user)
        {
            return UserRepository.Update(user);
        }
    }
}
