using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SD_330_W22SD_Assignment.Data;
using System.Linq;

namespace SD_330_W22SD_Assignment.Models
{
    public static class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context
                = new ApplicationDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            List<string> roles = new List<string>
            {
                "Administrator", "GroupAdmin", "User"
            };

            if (!context.Roles.Any())
            {
                foreach (string role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                ApplicationUser seedAdminUser = new ApplicationUser
                {
                    Email = "Admin@redditoverflow.com",
                    NormalizedEmail = "ADMIN@REDDITOVERFLOW.COM",
                    UserName = "Admin@redditoverflow.com",
                    NormalizedUserName = "ADMIN@REDDITOVERFLOW.COM",
                    EmailConfirmed = true,
                    Reputation = 0
                };

                var password1 = new PasswordHasher<ApplicationUser>();
                var hashed1 = password1.HashPassword(seedAdminUser, "P@ssW0rd");
                seedAdminUser.PasswordHash = hashed1;

                await userManager.CreateAsync(seedAdminUser);
                await userManager.AddToRoleAsync(seedAdminUser, "Administrator");

                ApplicationUser seedGroupAdminUser = new ApplicationUser
                {
                    Email = "GA@redditoverflow.com",
                    NormalizedEmail = "GA@REDDITOVERFLOW.COM",
                    UserName = "GA@redditoverflow.com",
                    NormalizedUserName = "GA@REDDITOVERFLOW.COM",
                    EmailConfirmed = true,
                    Reputation = 0
                };

                var password2 = new PasswordHasher<ApplicationUser>();
                var hashed2 = password2.HashPassword(seedGroupAdminUser, "P@ssW0rd");
                seedGroupAdminUser.PasswordHash = hashed2;

                await userManager.CreateAsync(seedGroupAdminUser);
                await userManager.AddToRoleAsync(seedGroupAdminUser, "GroupAdmin");

                ApplicationUser seedNormalUser = new ApplicationUser
                {
                    Email = "elon.musk@tesla.com",
                    NormalizedEmail = "ELON.MUSK@TESLA.COM",
                    UserName = "elon.musk@tesla.com",
                    NormalizedUserName = "ELON.MUSK@TESLA.COM",
                    EmailConfirmed = true,
                    Reputation = 0
                };

                var password3 = new PasswordHasher<ApplicationUser>();
                var hashed3 = password3.HashPassword(seedNormalUser, "P@ssW0rd");
                seedNormalUser.PasswordHash = hashed3;

                await userManager.CreateAsync(seedNormalUser);
                await userManager.AddToRoleAsync(seedNormalUser, "User");

                await context.SaveChangesAsync();
            }

            if(!context.Tag.Any())
            {
                Tag seedSoftwareTag = new Tag
                {
                    Name = "Software",
                    BgColor = "Coral",
                    Emoji = "128187"
                };

                Tag seedGamesTag = new Tag
                {
                    Name = "Games",
                    BgColor = "Lavender",
                    Emoji = "127918"
                };

                Tag seedFoodTag = new Tag
                {
                    Name = "Food",
                    BgColor = "Wheat",
                    Emoji = "127829"
                };

                Tag seedMoviesTag = new Tag
                {
                    Name = "Movies",
                    BgColor = "Plum",
                    Emoji = "127916"
                };

                Tag seedPetsTag = new Tag
                {
                    Name = "Pets",
                    BgColor = "LightSkyBlue",
                    Emoji = "128049"
                };

                Tag seedRelationshipTag = new Tag
                {
                    Name = "Relationship",
                    BgColor = "LightPink",
                    Emoji = "128147"
                };

                Tag seedHobbiesTag = new Tag
                {
                    Name = "Hobbies",
                    BgColor = "LightGreen",
                    Emoji = "128248"
                };

                Tag seedHardwareTag = new Tag
                {
                    Name = "Hardware",
                    BgColor = "LemonChiffon",
                    Emoji = "128296"
                };

                Tag seedHealthTag = new Tag
                {
                    Name = "Health",
                    BgColor = "LightCoral",
                    Emoji = "128657"
                };

                Tag seedSportsTag = new Tag
                {
                    Name = "Sports",
                    BgColor = "LightGray",
                    Emoji = "9917"
                };

                await context.Tag.AddAsync(seedFoodTag);
                await context.Tag.AddAsync(seedGamesTag);
                await context.Tag.AddAsync(seedHardwareTag);
                await context.Tag.AddAsync(seedHealthTag);
                await context.Tag.AddAsync(seedHobbiesTag);
                await context.Tag.AddAsync(seedMoviesTag);
                await context.Tag.AddAsync(seedPetsTag);
                await context.Tag.AddAsync(seedRelationshipTag);
                await context.Tag.AddAsync(seedSoftwareTag);
                await context.Tag.AddAsync(seedSportsTag);

                await context.SaveChangesAsync();

            }
        }
    }
}
