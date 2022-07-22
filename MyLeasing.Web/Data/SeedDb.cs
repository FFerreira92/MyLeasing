using MyLeasing.Web.Data.Entities;
using System;
using System.Collections.Generic;
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
        private readonly ILesseeRepository _lesseeRepository;

        private Random _random;

        public SeedDb(DataContext context,IUserHelper userHelper,ILesseeRepository lesseeRepository)
        {
            _context = context;
            _userHelper = userHelper;
            _lesseeRepository = lesseeRepository;

            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Lessee");

            var user = await _userHelper.GetUserByEmailAsync("FilipeFerreira@yopmail.com");

            if (user == null)
            {
                AddUser("Filipe","Ferreira","Montijo");

                user = await _userHelper.GetUserByEmailAsync("FilipeFerreira@yopmail.com");

                if (user == null)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
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

                var users = _userHelper.GetAllUsers().ToList();

                foreach (var owner in users)
                {
                    AddOwner(owner).Wait();

                    _userHelper.AddUserToRoleAsync(owner, "Owner").Wait();

                }
                await _context.SaveChangesAsync();
            }

            if (!_context.Lessee.Any())
            {
                string[] firstNames = new string[5] {"Gervasio","Gertrudes","Maria","Simao","Elon"};
                string[] lastNames = new string[5] { "Pereira", "Feiosa", "Catarro", "Guedes", "Musk" };
                string[] address = new string[5] {"Lisboa","Porto","Évora","Setúbal","Flórida"};

                for (int i = 0; i < 5; i++)
                {
                    AddUser(firstNames[i],lastNames[i],address[i]);
                    var fetchUser = await _userHelper.GetUserByEmailAsync(firstNames[i] + lastNames[i] + "@yopmail.com");
                    AddLessee(fetchUser).Wait();
                    _userHelper.AddUserToRoleAsync(fetchUser, "Lessee").Wait();
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

        private async Task AddLessee(User user)
        {
            if (user != null)
            {
                var phone = 0;

                if (user.PhoneNumber != null)
                {
                    phone = int.Parse(user.PhoneNumber);
                }

                var lessee = _context.Lessee.Add(new Lessee
                {
                    Document = int.Parse(user.Document),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FixedPhone = _random.Next(21999999),
                    CellPhone = phone,
                    User = user,
                    Address = user.Address
                });
            }
        }



    }
}

