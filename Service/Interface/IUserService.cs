using WidgetAndCo.Domain;
using WidgetAndCo.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Service.Interface
{
    public interface IUserService
    {
        User CreateUser(User user);
        User GetUser(int userId);
        User UpdateUser(User user);
        List<User> GetAllUsers();
        void DeleteUser(User user);

    }
}
