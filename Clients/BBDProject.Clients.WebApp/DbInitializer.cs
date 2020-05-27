using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Resources;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BBDProject.Clients.WebApp
{
    public class DbInitializer
    {
        private Dictionary<string, List<UserRegisterForm>> Users;

        public async Task Initialize(
            UserManager<DaoUser> userManager,
            RoleManager<DaoRole> roleManager)
        {
            Users = new Dictionary<string, List<UserRegisterForm>>();
            Users.Add(RoleNames.User, new List<UserRegisterForm>()
            {
                new UserRegisterForm() { FirstName = "Adam", LastName = "Jankowski", UserName = "user1", Password = "user1" },
                new UserRegisterForm() { FirstName = "Jan", LastName = "Adamski", UserName = "user2", Password = "user2" }
            });

            await FirstConfigUserRoles(userManager, roleManager);
        }

        private async Task CreateRole(string roleName, RoleManager<DaoRole> roleManager)
        {
            if (!(await roleManager.RoleExistsAsync(roleName)))
            {
                await roleManager.CreateAsync(new DaoRole { Name = roleName });
            }
        }

        private async Task FirstConfigUserRoles(UserManager<DaoUser> userManager, RoleManager<DaoRole> roleManager)
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
                            var user = new DaoUser
                            {
                                Email = $"{userRegisterForm.UserName}@clients.pl",
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
