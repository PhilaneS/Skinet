using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public static class AppIdentiryDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser 
                {
                    DisplayName = "PhilaneS",
                    Email = "kaizer.pk@gmail.com",
                    UserName = "kaizer.pk@gmail.com",
                    Address = new Address 
                    {
                        FirstName = "Philane",
                        LastName = "Sigwebela",
                        Street = "5611 Manqele Road Lamontvile",
                        City = "Durban",
                        State ="KwaZulu-Natal",
                        ZipCode="4027"
                    }
                };
                await userManager.CreateAsync(user,"Ayanda06#");
             
            }            
        }
     }
}