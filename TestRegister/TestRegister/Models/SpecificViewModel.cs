using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRegister.Business.Models;
using TestRegister.DAL.Entity;

namespace TestRegister.Models
{
    public class SpecificViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public Event Event { get; set; }
    }
}