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


        public Lessee ToLessee(LesseeViewModel model, string path, bool isNew)
        {
            return new Lessee()
            {
                Id = isNew ? 0 : model.Id,
                Document = model.Document,
                Photo = path,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Address = model.Address,
                User = model.User
            };
        }

        public LesseeViewModel ToLesseeViewModel(Lessee lessee)
        {
            return new LesseeViewModel()
            {
                Id = lessee.Id,
                Document = lessee.Document,
                Photo = lessee.Photo,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                FixedPhone = lessee.FixedPhone,
                CellPhone = lessee.CellPhone,
                Address = lessee.Address,
                User = lessee.User
            };
        }


    }
}
