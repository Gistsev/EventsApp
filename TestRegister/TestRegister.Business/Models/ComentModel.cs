using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRegister.Business.Models
{
    public class CommentModel
    {
        public int id;
        public int ownerId;
        public string ownerName;
        public bool isOwner;
        public DateTime time;
        public string msg;
    }
}
