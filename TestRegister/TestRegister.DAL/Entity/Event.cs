using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRegister.DAL.Entity
{
    
   public class Event : Entity
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public GroupEvent GroupEvent { get; set; }
        public User user { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
    }
 }
