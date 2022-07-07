using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("f92ferreira@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Filipe",
                    LastName = "Ferreira",
                    Email = "f92ferreira@gmail.com",
                    UserName = "f92ferreira@gmail.com",
                    Document = "36589789",
                    Address = "Montijo"
                };

                var result = await _userHelper.AddUserAsync(user, "Cinel123");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

            }


            if (!_context.Owners.Any())
            {
                AddOwner("Filipe", "Ferreira",user);
                AddOwner("Rafael", "Santos", user);
                AddOwner("Maria", "Callas", user);
                AddOwner("João", "Rodrigues", user);
                AddOwner("Rui", "Costa", user);
                AddOwner("Maria", "Lopes", user);
                AddOwner("Gonçalo", "Patricio", user);
                AddOwner("Eliane", "Santos", user);
                AddOwner("Sofia", "Jasus", user);
                AddOwner("Cristiano", "SemRonaldo", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string firstName,string lastName,User user)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(99999999),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(219999999),
                CellPhone = _random.Next(969999999),
                User = user
            }); ;
        }
    }
}

