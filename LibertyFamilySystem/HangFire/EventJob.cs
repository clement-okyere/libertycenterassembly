using Hangfire;
using LibertyFamilySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.HangFire
{
    public interface IEventJob
    {
        Task Run(IJobCancellationToken token);
        Task RunAtTimeOf(DateTime now);
    }
    public class EventJob: IEventJob
    {
        private readonly MembersDbContext _memberDb;

        public EventJob(MembersDbContext memberDb)
        {
            _memberDb = memberDb;
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.Now);
        }

        public async Task RunAtTimeOf(DateTime now)
        {
            //do work here
            DayOfWeek Today = DateTime.Today.DayOfWeek;
            var events = _memberDb.Event.ToList();
            events.ForEach(x => x.IsActive = false);
            _memberDb.SaveChanges();
            if (Today == DayOfWeek.Sunday)
            {
                var sundayEvent = _memberDb.Event.Where(x => x.Name.ToLower() == "sunday service").FirstOrDefault();
                sundayEvent.IsActive = true;
                await  _memberDb.SaveChangesAsync();
            }
            else if (Today == DayOfWeek.Wednesday)
            {
                var wednesdayEvent = _memberDb.Event.Where(x => x.Name.ToLower() == "bible study").FirstOrDefault();
                wednesdayEvent.IsActive = true;
                await _memberDb.SaveChangesAsync();
            }
            await _memberDb.SaveChangesAsync();
        }

    }
}
