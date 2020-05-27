using BBDProject.Management.Db.Dao;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Management.Models.Resources;
using BBDProject.Shared.Models.User;

namespace BBDProject.Management.WebApp
{
    public class DbInitializer
    {
        private Dictionary<string, List<UserRegisterForm>> Users;

        public async Task Initialize(
            UserManager<DaoEmployee> userManager,
            RoleManager<DaoRole> roleManager)
        {
            Users = new Dictionary<string, List<UserRegisterForm>>();
            Users.Add(RoleNames.Admin, new List<UserRegisterForm>() { new UserRegisterForm() { FirstName = "Adam", LastName = "Jankowski", UserName = "admin", Password = "admin" } });
            Users.Add(RoleNames.Moderator, new List<UserRegisterForm>() { new UserRegisterForm() { FirstName = "Jan", LastName = "Adamski", UserName = "mod", Password = "mod" } });

            await FirstConfigUserRoles(userManager, roleManager);
        }

        private async Task CreateRole(string roleName, RoleManager<DaoRole> roleManager)
        {
            if (!(await roleManager.RoleExistsAsync(roleName)))
            {
                await roleManager.CreateAsync(new DaoRole { Name = roleName });
            }
        }

        private async Task FirstConfigUserRoles(UserManager<DaoEmployee> userManager, RoleManager<DaoRole> roleManager)
        {
            foreach (var item in Users)
            {
                try
                {
                    await CreateRole(item.Key, roleManager);

                    foreach (var userRegisterForm in item.Value)
                    {
                        var userExists = await userManager.FindByNameAsync(userRegisterForm.UserName);

                        if (userExists == null)
                        {
                            var user = new DaoEmployee
                            {
                                Email = $"{userRegisterForm.UserName}@management.pl",
                                UserName = userRegisterForm.UserName,
                                EmailConfirmed = true,
                                FirstName = userRegisterForm.FirstName,
                                LastName = userRegisterForm.LastName
                            };

                            var result = await userManager.CreateAsync(user, userRegisterForm.Password);
                            await userManager.AddToRoleAsync(user, item.Key);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
