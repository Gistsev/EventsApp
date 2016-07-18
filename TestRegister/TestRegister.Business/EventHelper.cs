using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRegister.DAL;
using TestRegister.DAL.Entity;

namespace TestRegister.Business
{
    public class EventHelper
    {

        public EventHelper(IRepository repository)
        {
            this.context = repository;
            this.groups = new GroupEventHelper(this.context);
        }

        private IRepository context;
        private GroupEventHelper groups;

        public Event AddEvent(string title, string description, int groupID)
        {
            var d = new Event() { Title = title, Description = description };
            d.GroupEvent = groups.GetGroup(groupID);
            context.Add(d);
            context.SaveChanges();
            return d;
        }

        public void DeleteEvent(int id)
        {
                var d = GetEvent(id);
            context.Delete(d);
                context.SaveChanges();
        }

       
        public IEnumerable<Event> GetEvents()
        {
            return context.Events;
        }

        public IEnumerable<Event> GetEvents(int groupID)
        {
            return context.Events.Where(x => x.GroupEvent != null && x.GroupEvent.Id == groupID).ToList();
        }

        public Event GetEvent(int id)
        {
            return GetEvents().FirstOrDefault(x => x.Id == id);
        }
    }
}
