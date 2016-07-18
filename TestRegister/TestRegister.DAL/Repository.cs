using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestRegister.DAL.Entity;

namespace TestRegister.DAL
{
    public class Repository : IRepository
    {
        private DataContext context = new DataContext();

        public IEnumerable<User> Users
        {
            get { return context.UserProfiles.ToList(); }
        }

        public IEnumerable<GroupEvent> GroupEvents
        {
            get
            {
                return context.GroupEvents.ToList();
            }
        }

        public IEnumerable<Event> Events
        {
            get
            {
                return context.Events.Include("GroupEvent").ToList();
            }
        }

        public IEnumerable<Comment> Comments
        {
            get { return context.Comments.Include("Event").ToList(); }
        } 

        public IEnumerable<T> Get<T>() where T : TestRegister.DAL.Entity.Entity
        {
            return this.context.Set<T>();
        }

        public void Add<T>(T entity) where T : TestRegister.DAL.Entity.Entity
        {
            this.context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : TestRegister.DAL.Entity.Entity
        {
            this.context.Set<T>().Remove(entity);
        }

        public void addUser(User user)
        {
            context.UserProfiles.Add(user);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public static void initDB()
        {
            Database.SetInitializer<DataContext>(null);
            using (var context = new DataContext())
            {
                if (!context.Database.Exists())
                {
                    // Create the SimpleMembership database without Entity Framework migration schema
                    ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                }
            }
        }
    }
}
