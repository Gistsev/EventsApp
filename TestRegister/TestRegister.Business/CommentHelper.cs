using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRegister.DAL;
using TestRegister.DAL.Entity;

namespace TestRegister.Business
{
    public class CommentHelper
    {
        public CommentHelper(IRepository repository)
        {
            this.context = repository;
            this.events = new EventHelper(this.context);
        }

        private IRepository context;
        private EventHelper events;

        public IEnumerable<Comment> GetComments()
        {
            return context.Comments;
        }

        public IEnumerable<Comment> GetComments(int eventId)
        {
            return context.Comments.Where(x => x.Event != null && x.Event.Id == eventId).ToList();
        }

        public Comment GetComment(int _id)
        {
            return GetComments().First(x => x.Id == _id);
        }

        public Comment AddComment(string msg, int eventID)
        {
            var d = new Comment();
            d.Message = msg;
            d.Time = DateTime.Now;
            d.Event = events.GetEvent(eventID);
            context.Add<Comment>(d);
            context.SaveChanges();
            return d;
        }

        public void DeleteComment(int id)
        {
            var d = GetComment(id);
            context.Delete(d);
            context.SaveChanges();
        }
    }
}
