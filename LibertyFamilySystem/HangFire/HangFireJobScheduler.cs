using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.HangFire
{
    public class HangFireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {
           RecurringJob.RemoveIfExists(nameof(EventJob));
            RecurringJob.AddOrUpdate<EventJob>(nameof(EventJob),
                job => job.Run(JobCancellationToken.Null),
                Cron.Daily(1, 00), TimeZoneInfo.Local);

        }
    }
}
