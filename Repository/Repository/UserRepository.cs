using WidgetAndCo.DAL;
using WidgetAndCo.Domain;
using WidgetAndCo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WidgetAndCo.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private WidgetAndCoContext Context;
        public UserRepository(WidgetAndCoContext context)
        {
            Context = context;
        }

        public User Add(User entity)
        {
            Context.User.Add(entity);
            Commit();
            return entity;
        }

        public IEnumerable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            Context.Remove(entity);
            Commit();
        }

        public void DeleteWhere(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindBy(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetSingle(int id)
        {
            User user = Context.User.Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public User GetSingle(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetSingle(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public User Update(User entity)
        {
            Context.User.Update(entity);
            Commit();
            return entity;
        }

        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();
            allUsers = Context.Set<User>().ToList();
            return allUsers;
        }
    }
}
