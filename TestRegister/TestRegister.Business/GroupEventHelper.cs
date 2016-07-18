using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRegister.DAL;
using TestRegister.DAL.Entity;

namespace TestRegister.Business
{
    public class GroupEventHelper
    {
        private IRepository context;

        public GroupEventHelper(IRepository repository)
        {
            this.context = repository;
        }

        public IEnumerable<GroupEvent> GetGroups()
        {
            return context.GroupEvents.Select(x =>
            {
                x.EventsCount = context.Events.Count(s => s.GroupEvent.Id == x.Id);
                return x;
            });
        }

        public GroupEvent GetGroup(int id)
        {
            return GetGroups().FirstOrDefault(x => x.Id == id);
        }
    }
}
