using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRegister.DAL.Entity;

namespace TestRegister.Models
{
    public class EventViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public GroupEvent GroupEvent { get; set; }
    }
}