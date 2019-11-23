using LibertyFamilySystem.Data;
using LibertyFamilySystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Repositories
{
    public interface IDashboardRepository
    {
        DashboardViewModel GetStatistics();
    }
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MembersDbContext _memberContext;
      
        public DashboardRepository(MembersDbContext memberContext)
        {
            _memberContext = memberContext;
        }
        public DashboardViewModel GetStatistics()
        {
            var members = _memberContext.Member.Count();
            var groups = _memberContext.Group.Count();

            return new DashboardViewModel
            {
                 Groups = groups,
                 Members = members
            };
        }      
    }
}
