using System.Linq;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class LesseeRepository : GenericRepository<Lessee>,ILesseeRepository
    {
        private readonly DataContext _context;

        public LesseeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}
