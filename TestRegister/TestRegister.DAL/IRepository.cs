using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRegister.DAL.Entity;

namespace TestRegister.DAL
{
    public interface IRepository
    {
        
        IEnumerable<User> Users { get; }
        IEnumerable<Event> Events { get; }
        IEnumerable<GroupEvent> GroupEvents { get; }
        IEnumerable<Comment> Comments { get; }

        IEnumerable<T> Get<T>() where T : TestRegister.DAL.Entity.Entity;

        void Add<T>(T entity) where T : TestRegister.DAL.Entity.Entity;

        void Delete<T>(T entity) where T : TestRegister.DAL.Entity.Entity;

        void SaveChanges();

        void Dispose();

    }
}
