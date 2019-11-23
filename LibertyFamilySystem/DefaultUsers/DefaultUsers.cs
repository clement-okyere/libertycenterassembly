using LibertyFamilySystem.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.DefaultUsers
{
    public class DefaultUsers
    {
        public enum Role
        {
            Admin,
            GroupAdmin
        }

        public static async Task CreateDefaultUsers(ApplicationDbContext _context, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            // Get the list of the roles from the enum
            Role[] roles = (Role[])Enum.GetValues(typeof(Role));
            foreach (var r in roles)
            {
                // Create an identity role object out of the enum value
                var identityRole = new IdentityRole
                {
                    Name = r.ToString()
                };

                // Create the role if it doesn't already exist
                if (!await _roleManager.RoleExistsAsync(roleName: identityRole.Name))
                {
                    var result = await _roleManager.CreateAsync(identityRole);

                    if (!result.Succeeded)
                        throw new Exception(string.Join(".", result.Errors.Select(x => x.Description)));
                }
            }

            #region Admin User
            //Admin user
            IdentityUser adminUser = new IdentityUser
            {
                UserName = "admin"
            };

            // Add the admin user to the database if it doesn't already exist
            if (await _userManager.FindByNameAsync(adminUser.UserName) == null)
            {
                // WARNING: Do NOT check in credentials of any kind into source control
                var result = await _userManager.CreateAsync(adminUser, "Admin@12345");

                if (!result.Succeeded) // Return 500 if it fails
                    throw new Exception(string.Join(".", result.Errors.Select(x => x.Description)));

                // Assign Administrator Role to Admin User
                result = await _userManager.AddToRoleAsync(adminUser, Role.Admin.ToString());

                if (!result.Succeeded)
                    throw new Exception(string.Join(".", result.Errors.Select(x => x.Description)));

            }
            #endregion
        }        
    }
}
