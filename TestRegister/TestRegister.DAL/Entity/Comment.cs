using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRegister.DAL.Entity
{
    [Table("Comment")]
    public class Comment : Entity
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
