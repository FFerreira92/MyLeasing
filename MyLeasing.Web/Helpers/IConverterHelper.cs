using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public interface IConverterHelper
    {
        Owner toOwner(OwnerViewModel model, string path, bool isNew);

        OwnerViewModel toOwnerViewModel(Owner owner);
    }
}
