using LibertyFamilySystem.Data;
using LibertyFamilySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace LibertyFamilySystem.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> LoadMembers(string status, int memberId);
        Task<KeyValuePair<bool, string>> Add(Member member);
        Task<KeyValuePair<bool, string>> Update(Member member);
        IEnumerable<Member> GetMemberByGroup(int groupId);
        Member GetMemberByPhoneNumber(string phoneNumber);
    }
    public class MemberRepository: IMemberRepository
    {
        private readonly MembersDbContext _memberContext;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public MemberRepository(MembersDbContext memberContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _memberContext = memberContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<Member>> GetMembers()
        {
            var members = _memberContext.Member
                          .Include(x=> x.Group)
                          .Include(x => x.Occupation)
                          .ToList();
            return members;
        }

        public  IEnumerable<Member> GetMemberByGroup(int groupId)
        {
            var members =  _memberContext.Member
                          .Include(x => x.Group)
                          .Include(x => x.Occupation)
                          .Where(x => x.GroupId == groupId)
                          .ToList();
            return members;
        }

        public Member GetMemberByPhoneNumber(string phoneNumber)
        {
            var member = _memberContext.Member
                          .Include(x => x.Group)
                          .Include(x => x.Occupation)
                          .Where(x => x.PhoneNumber == phoneNumber)
                          .FirstOrDefault();
            return member;
        }

        public async Task<Member> LoadMembers(string status, int memberId)
        {
            var groups = _memberContext.Group.Select(x => new
            {
                GroupId = x.GroupId,
                Name = x.Name
            });

            var occupations = _memberContext.Occupation.Select(x => new
            {
                OccupationId = x.OccupationId,
                Name = x.Name
            });

            var member = new Member();

            if (status == "add")
            {
                member.GroupSel = new SelectList(groups, "GroupId", "Name");
                member.OccupationSel = new SelectList(occupations, "OccupationId", "Name");
                return member;
            }
            else if (status == "update" && (memberId != null))
            {
               member = _memberContext.Member
                           .Include(x => x.Group)
                           .Include(x => x.Occupation)
                           .Where(x => x.MemberId == memberId)
                           .FirstOrDefault();

                member.GroupSel = new SelectList(groups, "GroupId", "Name", member.GroupId);
                member.OccupationSel = new SelectList(occupations, "OccupationId", "Name", member.OccupationId);
                return member;
            }

            return member;
        }

        public async Task<KeyValuePair<bool, string>> Add(Member  member)
        {
             await _memberContext.Member.AddAsync(member);
             await _memberContext.SaveChangesAsync();

            var username = member.FirstName.Substring(0, 4) + member.LastName.Substring(0, 4) + "123";
            var password = "Admin@12345";

            if (member.IsGroupAdmin)
            {
                var user = new IdentityUser()
                {
                    PhoneNumber = member.PhoneNumber,
                    UserName = username
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                    return new KeyValuePair<bool, string>(false, string.Join(".", result.Errors.Select(x => x.Description)));

                result = await _userManager.AddToRoleAsync(user, "GroupAdmin");
                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return new KeyValuePair<bool, string>(false, string.Join(".", result.Errors.Select(x => x.Description)));
                }
            }
            return new KeyValuePair<bool, string>(true, "Added Successfully");
        }

        public async Task<KeyValuePair<bool, string>> Update(Member memberN)
        {

            var member = _memberContext.Member.Find(memberN.MemberId);
            member.FirstName = memberN.FirstName;
            member.LastName = memberN.LastName;
            member.MiddleName = memberN.MiddleName;
            member.IsGroupAdmin = memberN.IsGroupAdmin;
            member.PhoneNumber = memberN.PhoneNumber;
            member.WhatsAppNumber = memberN.WhatsAppNumber;
            member.OccupationId = memberN.OccupationId;
            member.GroupId = memberN.GroupId;
            await _memberContext.SaveChangesAsync();
           

            if (member.IsGroupAdmin)
            {
                var username = member.FirstName.Substring(0, 4) + member.LastName.Substring(0, 4) + "123";
                var password = "Admin@12345";
                var userExists = await _userManager.FindByNameAsync(username);
                if(userExists != null)
                    return new KeyValuePair<bool, string>(true, "Added Successfully");

                var user = new IdentityUser()
                {
                    PhoneNumber = member.PhoneNumber,
                    UserName = username
                };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                    return new KeyValuePair<bool, string>(false, string.Join(".", result.Errors.Select(x => x.Description)));

                result = await _userManager.AddToRoleAsync(user, "GroupAdmin");
                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return new KeyValuePair<bool, string>(false, string.Join(".", result.Errors.Select(x => x.Description)));
                }
            }
            else
            {
                var username = member.FirstName.Substring(0, 4) + member.LastName.Substring(0, 4) + "123";
                var password = "Admin@12345";
                var userExists = await _userManager.FindByNameAsync(username);
                if (userExists != null)
                    return new KeyValuePair<bool, string>(true, "Added Successfully");

                var results = await _userManager.RemoveFromRoleAsync(userExists, "GroupAdmin");
                if(results.Succeeded)
                    return new KeyValuePair<bool, string>(true, "Updated Successfully");
                else
                    return new KeyValuePair<bool, string>(false, "Something went wrong, Please Contact Admistrator");

            }
            return new KeyValuePair<bool, string>(true, "Updated Successfully");
        }
    }
}
