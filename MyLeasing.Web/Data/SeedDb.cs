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

            var user = await _userHelper.GetUserByEmailAsync("FilipeFerreira@yopmail.com");

            if (user == null)
            {
                AddUser("Filipe","Ferreira","Montijo");

                var result = await _userHelper.AddUserAsync(user, "Cinel123");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

            }

            if (!_context.Owners.Any())
            {
                AddUser("Raquel", "Ferreira","Montijo");
                AddUser("Rafael", "Santos","Lisboa");
                AddUser("Maria", "Callas","Porto");
                AddUser("Joao", "Rodrigues","Setubal");
                AddUser("Rui", "Costa","Braga");
                AddUser("Maria", "Lopes","Lisboa");
                AddUser("Goncalo", "Patricio","Porto");
                AddUser("Eliane", "Santos","Porto");
                AddUser("Sofia", "Jasus","Leiria");
                AddUser("Cristiano", "SemRonaldo","Cascais");

                //var users = _context.Users.AsQueryable().ToList();
                var users = _userHelper.GetAllUsers();
                foreach (var owner in users)
                {
                    AddOwner(owner).Wait();
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
                Document = _random.Next(9999999).ToString(),
                Address = address,
                PhoneNumber = _random.Next(96999999).ToString()
            },"123456").Wait();

        }

        private async Task AddOwner(User user)
        
        {
            if (user != null)
            {
                var phone = 0;

                if (user.PhoneNumber != null)
                {
                    phone = int.Parse(user.PhoneNumber);
                }

                var owner = _context.Owners.Add(new Owner
                {
                    Document = int.Parse(user.Document),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FixedPhone = _random.Next(21999999),
                    CellPhone = phone,
                    User = user
                });
            }
        }
    }
}

