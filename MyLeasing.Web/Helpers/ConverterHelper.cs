using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Owner toOwner(OwnerViewModel model, string path, bool isNew)
        {
            return new Owner()
            {
                Id = isNew ? 0 : model.Id,
                Document = model.Document,
                ImageUrl = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Adress = model.Adress,
                User = model.User
            };
        }

        public OwnerViewModel toOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                Document = owner.Document,
                ImageUrl = owner.ImageUrl,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixedPhone = owner.FixedPhone,
                CellPhone = owner.CellPhone,
                Adress = owner.Adress,
                User = owner.User
            };
        }
    }
}
