using LibertyFamilySystem.Data;
using LibertyFamilySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Repositories
{
    public interface IEventRepository
    {
        Event GetActiveEvent();
    }
    public class EventRepository : IEventRepository
    {
        private MembersDbContext _membersDb;

        public EventRepository(MembersDbContext membersDb)
        {
            _membersDb = membersDb;
        }

        public Event GetActiveEvent()
        {
            var activeEvent = _membersDb.Event.Where(x => x.IsActive == true).FirstOrDefault();
            return activeEvent ?? new Event();
        }
    }
}
