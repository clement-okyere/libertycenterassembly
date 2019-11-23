using LibertyFamilySystem.Data;
using LibertyFamilySystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Repositories
{

    public interface IUserRepository
    {
        Task<List<UserViewModel>> GetUsers();
        Task<UserViewModel> LoadUsers(string status, string username);
        Task<KeyValuePair<bool, string>> Add(UserViewModel userV);
        Task<KeyValuePair<bool, string>> Update(UserViewModel userV);
    }
    public class UserRepository: IUserRepository
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<UserViewModel>> GetUsers()
        {
            List<UserViewModel> userViewModel = new List<UserViewModel> { };
            var users = _userManager.Users.ToList();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserViewModel userV = new UserViewModel();
                userV.Username = user.UserName;
                userV.Role = string.Empty;
                foreach(var role in roles)
                {
                    if (!userV.Role.Contains(","))
                    {
                        userV.Role = role;
                    }
                    else
                    {
                        userV.Role = "," + role;
                    }
                }
                userViewModel.Add(userV);
            }
            return userViewModel;
        }

        public async Task<UserViewModel> LoadUsers(string status, string username)
        {
            if (status == "add")
            {
                UserViewModel user = new UserViewModel();
                return user;
            }
            else if (status == "update" && (username != null))
            {
                var user = await _userManager.FindByNameAsync(username);
                UserViewModel userV = new UserViewModel();
                userV.Username = user.UserName;
                userV.PhoneNumber = user.PhoneNumber;
                userV.Role = string.Empty;
                userV.IsActive = user.LockoutEnabled == true ? false : true;
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    if (!userV.Role.Contains(","))
                    {
                        userV.Role = role;
                    }
                    else
                    {
                        userV.Role = "," + role;
                    }
                }
                return userV;
            }
            else return new UserViewModel();

        }

        public async Task<KeyValuePair<bool, string>> Add(UserViewModel userV)
        {
            var user = new IdentityUser()
            {
                PhoneNumber = userV.PhoneNumber,
                UserName = userV.Username,
                LockoutEnabled = (userV.IsActive) ? false : true,
                LockoutEnd = (userV.IsActive) ? new DateTime(3000, 12, 01) : DateTime.Now
            };

            string password = "Admin@12345";
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return new KeyValuePair<bool, string>(false, string.Join(".", result.Errors.Select(x => x.Description)));

            result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new KeyValuePair<bool, string>(false, string.Join(".", result.Errors.Select(x => x.Description)));
            }

            return new KeyValuePair<bool, string>(true, "Added Successfully");
        }

        public async Task<KeyValuePair<bool, string>> Update(UserViewModel userV)
        {
            var user = await _userManager.FindByNameAsync(userV.Username);

            user.PhoneNumber = userV.PhoneNumber;
            user.LockoutEnabled = (userV.IsActive) ? false : true;
            user.LockoutEnd = (userV.IsActive) ? new DateTime(3000, 12, 01) : DateTime.Now;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new KeyValuePair<bool, string>(false, string.Join(".", result.Errors.Select(x => x.Description)));

            return new KeyValuePair<bool, string>(true, "User Updated Successfully");
        }
    }
}
