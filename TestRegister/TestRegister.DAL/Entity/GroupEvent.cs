using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace TestRegister.DAL.Entity
{
    public class GroupEvent : Entity
    {
        public string Name { get; set; }

        [NotMapped]
        public int EventsCount { get; set; }
    }
}
