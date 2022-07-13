using System.Linq;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public interface ILesseeRepository : IGenericRepository<Lessee>
    {
        public IQueryable GetAllWithUsers();
    }
}
