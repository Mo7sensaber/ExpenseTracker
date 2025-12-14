using Domain.Model.IdentityMedule;
using Domain.RepoInterface;
using Microsoft.AspNetCore.Identity;
using Peresistance.Context;
using Peresistance.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistance
{
    public class DataSeed(ExpenceContext context, UserManager<ApplicationUser> user,
        RoleManager<IdentityRole> role, IdContext idContext) : IDataSeed
    {
        public async Task IdentityDataSeed()
        {
            if (!role.Roles.Any())
            {
                await role.CreateAsync(new IdentityRole("Admin"));
                await role.CreateAsync(new IdentityRole("SuperUser"));
            }

            if (!user.Users.Any())
            {
                var user1 = new ApplicationUser
                {
                    FullName = "Mohsen Saber",
                    Email = "mohsensaber001@gmail.com",
                    UserName = "MohsenSaber",
                    PhoneNumber = "01032999016"
                };

                var user2 = new ApplicationUser
                {
                    FullName = "Ziad Sobhy",
                    Email = "Ziad@gmail.com",
                    UserName = "ZiadSobhy",
                    PhoneNumber = "01023226538"
                };

                var result1 = await user.CreateAsync(user1, "Mohsen1234#");
                var result2 = await user.CreateAsync(user2, "Ziad1234#");

                if (result1.Succeeded)
                {
                    var savedUser1 = await user.FindByEmailAsync(user1.Email);
                    await user.AddToRoleAsync(savedUser1!, "Admin");
                }

                if (result2.Succeeded)
                {
                    var savedUser2 = await user.FindByEmailAsync(user2.Email);
                    await user.AddToRoleAsync(savedUser2!, "SuperUser");
                }

                // ✅ مهم جدًا
                await idContext.SaveChangesAsync();
            }
        }


    }
}
