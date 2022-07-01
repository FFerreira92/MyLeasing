using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Owners.Any())
            {
                AddOwner("Filipe", "Ferreira");
                AddOwner("Rafael", "Santos");
                AddOwner("Maria", "Callas");
                AddOwner("João", "Rodrigues");
                AddOwner("Rui", "Costa");
                AddOwner("Maria", "Lopes");
                AddOwner("Gonçalo", "Patricio");
                AddOwner("Eliane", "Santos");
                AddOwner("Sofia", "Jasus");
                AddOwner("Cristiano", "SemRonaldo");
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string firstName,string lastName)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(99999999),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(219999999),
                CellPhone = _random.Next(969999999)                
            }); ;
        }
    }
}

