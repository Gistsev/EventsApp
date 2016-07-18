using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRegister.DAL.Entity;

namespace TestRegister.DAL
{
    internal class DataContext : DbContext
    {
        public DataContext()
            : base("IdentityDb")
        {
        }

        public DbSet<User> UserProfiles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GroupEvent> GroupEvents { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
