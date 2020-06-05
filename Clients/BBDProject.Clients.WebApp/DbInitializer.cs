using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BBDProject.Clients.Db.Dao;
using BBDProject.Clients.Models.Product;
using BBDProject.Clients.Models.Resources;
using BBDProject.Clients.Repositories.Product;
using BBDProject.Shared.Models.User;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Core;

namespace BBDProject.Clients.WebApp
{
    public class DbInitializer
    {
        private Dictionary<string, List<UserRegisterForm>> Users;

        public async Task Initialize(
            UserManager<DaoUser> userManager,
            RoleManager<DaoRole> roleManager,
            IProductRepository productRepository)
        {
            Users = new Dictionary<string, List<UserRegisterForm>>();
            Users.Add(RoleNames.User, new List<UserRegisterForm>()
            {
                new UserRegisterForm() { FirstName = "Adam", LastName = "Jankowski", UserName = "user1", Password = "user1" },
                new UserRegisterForm() { FirstName = "Jan", LastName = "Adamski", UserName = "user2", Password = "user2" }
            });

            await FirstConfigUserRoles(userManager, roleManager);

            await AddProducts(productRepository, @"C:\Users\fglowacki\Pictures\planszowki");
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
                                LastName = userRegisterForm.LastName,
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


        private async Task AddProducts(IProductRepository productRepository, string path)
        {
            foreach (var filePath in Directory.GetFiles(path))
            {
                try
                {
                    var name = Path.GetFileNameWithoutExtension(filePath).Replace("_", " ");
                    var product = await productRepository.Get(name);
                    if (product == null)
                    {
                        await productRepository.Create(new ProductForm()
                        {
                            Name = name,
                            Brand = $"Gra planszowa",
                            Description =
                                "Gra planszowa w której chodzi o to, żeby wygrać. nie wolno jedynie przegrywać! Dobra zabawa i dużo jedzenia gwarantowane.",
                            Model = "II",
                            Image = await File.ReadAllBytesAsync(filePath)
                        });
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
