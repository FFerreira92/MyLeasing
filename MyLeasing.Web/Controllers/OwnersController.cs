using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _repository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public OwnersController(IOwnerRepository repository,IUserHelper userHelper,IBlobHelper blobHelper,IConverterHelper converterHelper)
        {
            _repository = repository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _repository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {

                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                }

                var owner = _converterHelper.toOwner(model, imageId, true);

                owner.User = new User
                {
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Email = owner.FirstName + owner.LastName + "@yopmail.com",
                    UserName = owner.FirstName + owner.LastName + "@yopmail.com",
                    Document = owner.Document.ToString(),
                    Address = owner.Adress,
                    PhoneNumber = owner.CellPhone.ToString(),
                };

                await _repository.CreateAsync(owner);
                await _userHelper.AddUserToRoleAsync(owner.User, "Owner");

                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }


        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _repository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            var model = _converterHelper.toOwnerViewModel(owner);

            return View(model);
        }


        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                    }

                    var owner = _converterHelper.toOwner(model, imageId, false);

                    owner.User = await _userHelper.GetUserByEmailAsync("FilipeFerreira@yopmail.com");

                    await _repository.UpdateAsync(owner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _repository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _repository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(owner);
            return RedirectToAction(nameof(Index));
        }
      
    }
}
