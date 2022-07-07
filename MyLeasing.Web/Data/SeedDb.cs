using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
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
                AddUser("Filipe", "Ferreira","Montijo");
                AddUser("Rafael", "Santos","Lisboa");
                AddUser("Maria", "Callas","Porto");
                AddUser("João", "Rodrigues","Setubal");
                AddUser("Rui", "Costa","Braga");
                AddUser("Maria", "Lopes","Lisboa");
                AddUser("Gonçalo", "Patricio","Porto");
                AddUser("Eliane", "Santos","Porto");
                AddUser("Sofia", "Jasus","Leiria");
                AddUser("Cristiano", "SemRonaldo","Cascais");
                var users = _context.Users.AsQueryable().ToList();
                foreach (var owner in users)
                {
                    AddOwner(owner);
                }
                await _context.SaveChangesAsync();
            }
        }

        private void AddUser(string firstName,string lastName,string address)
        {
            _userHelper.AddUserAsync(new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = firstName+lastName+"@yopmail.com",
                UserName = firstName + lastName + "@yopmail.com",
                Document = _random.Next(99999999).ToString(),
                Address = address,
                PhoneNumber = _random.Next(969999999).ToString()
            },"123456");
        }

        private void AddOwner(User user)
        {
            _context.Owners.Add(new Owner
            {
                Document = int.Parse(user.Document),
                FirstName = user.FirstName,
                LastName = user.LastName,
                FixedPhone = _random.Next(219999999),
                CellPhone = int.Parse(user.PhoneNumber),
            }); ;
        }
    }
}

