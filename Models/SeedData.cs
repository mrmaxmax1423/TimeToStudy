using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeToStudy.Models
{
    public class SeedData
    {
        private readonly EventContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SeedData(EventContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            // Ensure the database is created and up-to-date
            _context.Database.EnsureCreated();

            // Check if there are any users in the database
            if (!_context.Users.Any())
            {
                // Create a new user with the given username and password
                var user = new IdentityUser
                {
                    UserName = "your-username",
                    Email = "your-email"
                };
                var result = await _userManager.CreateAsync(user, "your-password");

                // If creating the user was successful, add them to the "Admin" role
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    throw new InvalidOperationException("Could not create user in SeedData");
                }
            }
        }
    }
}