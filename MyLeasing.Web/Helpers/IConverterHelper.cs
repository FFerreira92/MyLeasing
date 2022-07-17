using System;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public interface IConverterHelper
    {
        Owner toOwner(OwnerViewModel model, Guid imageId, bool isNew);

        OwnerViewModel toOwnerViewModel(Owner owner);

        Lessee ToLessee(LesseeViewModel model, Guid imageId, bool isNew);

        LesseeViewModel ToLesseeViewModel(Lessee lessee);
    }
}
