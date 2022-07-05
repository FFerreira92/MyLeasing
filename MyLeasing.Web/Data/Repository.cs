using MyLeasing.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Owner> GetOwners()
        {
            return _dataContext.Owners.OrderBy(o => o.FirstName);
        }

        public Owner GetOwner(int id)
        {
            return _dataContext.Owners.Find(id);
        }

        public void AddOwner(Owner owner)
        {
            _dataContext.Owners.Add(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            _dataContext.Owners.Update(owner);
        }

        public void RemoveOwner(Owner owner)
        {
            _dataContext.Owners.Remove(owner);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public bool OwnerExists(int id)
        {
            return _dataContext.Owners.Any(o => o.Id == id);
        }

    }
}
