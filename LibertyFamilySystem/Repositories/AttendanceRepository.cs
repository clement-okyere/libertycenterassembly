using LibertyFamilySystem.Models.Options;
using LibertyFamilySystem.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LibertyFamilySystem.Models;
using LibertyFamilySystem.Data;

namespace LibertyFamilySystem.Repositories
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<AttendanceViewModel>> GetAttendanceSheet(int groupId);
        Task<KeyValuePair<bool, string>> MarkAttendace(int memberId);
        int MemberEventAttendanceCount(int memberId);
        Task<int> UpdateMemberEventCount(int eventId, int memberId);
    }
    public class AttendanceRepository: PgRepository, IAttendanceRepository
    {
        private IEventRepository _eventRepository;
        private MembersDbContext _membersDbContext;

        public AttendanceRepository(IOptionsSnapshot<ConnectionOptions> options, IEventRepository eventRepository,
            MembersDbContext membersDbContext) : base(options)
        {
            _eventRepository = eventRepository;
            _membersDbContext = membersDbContext;
        }

        public async Task<IEnumerable<AttendanceViewModel>> GetAttendanceSheet(int groupId)
        {
            using (var c = await GetConnection())
            {
                var query = @"
                            select a.""EventId"",m.""MemberId"",Concat(m.""FirstName"",' ',m.""MiddleName"",' ',m.""LastName"") as FullName
                                from ""Attendance"" a right outer join ""Member""
                                m on a.""MemberId"" = m.""MemberId""
                                and m.""GroupId"" = @GroupId
                                and a.""EventId"" = (select ""EventId"" from ""Event"" where ""IsActive"" = true)";

                return await c.QueryAsync<AttendanceViewModel>(query, new {
                   GroupId = groupId
                }).ConfigureAwait(false);
            }
        }

        public async Task<int> UpdateMemberEventCount(int eventId, int memberId)
        {
            using (var c = await GetConnection())
            {
                var query = @"
                           DELETE FROM ""Attendance"" WHERE 
                            ""MemberId"" = @MemberId AND ""EventId"" = @EventID
                              RETURNING ""AttendanceId""";

                return await c.QueryFirstAsync<int>(query, new
                {
                    MemberId = memberId,
                    EventId = eventId
                });
            }
        }

        public int MemberEventAttendanceCount(int memberId)
        {
            try
            {
                var eventId = _eventRepository.GetActiveEvent().EventId;
                var count = _membersDbContext.Attendance
                            .Where(x => x.MemberId == memberId && x.EventId == eventId)
                            .Count();

              return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<KeyValuePair<bool, string>> MarkAttendace(int memberId)
        {
            try
            {
                var eventId = _eventRepository.GetActiveEvent().EventId;

                var count = MemberEventAttendanceCount(memberId);
                if (count > 0)
                {
                    await UpdateMemberEventCount(eventId, memberId);
                }
                else
                {
                    var Attendance = new Attendance
                    {
                        EventId = eventId,
                        MemberId = memberId
                    };
                    await _membersDbContext.Attendance.AddAsync(Attendance);
                    await _membersDbContext.SaveChangesAsync();
                }

                string response = (count == 0) ? "mark" : "unmark";        
                return new KeyValuePair<bool, string>(true, response);
            }
            catch (Exception ex)
            {

                return new KeyValuePair<bool, string>(false, ex.Message);
            }
        }
        
    }
}
