using Gaming.DAL;
using Gaming.Entities;
using Gaming.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gaming.Controllers
{
    public class ContactController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly GamingDbContext _dbContext;

        public ContactController(UserManager<User> userManager, GamingDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    CreationTime = DateTime.Now
                };

                _dbContext.Contacts.Add(contact);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Your message has been successfully submitted. We will get back to you shortly.";

                return RedirectToAction("Contact");
            }

            return View(model);
        }


    }
}