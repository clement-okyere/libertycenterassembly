using LibertyFamilySystem.Data;
using LibertyFamilySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Repositories
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetGroup();
        Group LoadGroups(string status, int groupId);
        Task<KeyValuePair<bool, string>> Add(Group group);
        Task<KeyValuePair<bool, string>> Update(Group groupV);
    }
    public class GroupRepository : IGroupRepository
    {
        private MembersDbContext _membersDb;

        public GroupRepository(MembersDbContext membersDb)
        {
            _membersDb = membersDb;
        }

        public IEnumerable<Group> GetGroup()
        {
            var groups = _membersDb.Group.ToList();
            return groups;
        }

        public Group LoadGroups(string status, int groupId)
        {
          
            var group = new Group();

            if (status == "add")
            {
                return group;
            }
            else if (status == "update" && (groupId != null))
            {
                group = _membersDb.Group.Find(groupId);
                return group;
            }

            return group;
        }

        public async Task<KeyValuePair<bool, string>> Add(Group group)
        {
            await _membersDb.Group.AddAsync(group);
            await _membersDb.SaveChangesAsync();
            return new KeyValuePair<bool, string>(true, "Added Successfully");
        }

        public async Task<KeyValuePair<bool, string>> Update(Group groupV)
        {
            var group = _membersDb.Group.Find(groupV.GroupId);
            group.Name = groupV.Name;
            await _membersDb.SaveChangesAsync();
            return new KeyValuePair<bool, string>(true, "Updated Successfully");
        }
    }
}
