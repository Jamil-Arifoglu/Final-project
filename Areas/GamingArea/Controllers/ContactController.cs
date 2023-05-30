using Gaming.DAL;
using Gaming.Entities;
using Gaming.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
    [Area("GamingArea")]
    public class ContactController : Controller
    {
        private readonly GamingDbContext _context;

        public ContactController(GamingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Contact> instruction = _context.Contacts.AsEnumerable();
            return View(instruction);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Contact newContact)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return View();
            }
            bool isDuplicated = _context.Contacts.Any(d => d.Address == newContact.Address && d.Callus == newContact.Callus && d.Email == newContact.Email);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Contacts.Add(newContact);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact is null) return NotFound();
            return View(contact);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Contact edited)
        {
            if (id == 0) return NotFound();
            Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact is null) return NotFound();

            bool duplicate = _context.Contacts.Any(d => d.Email == edited.Email && d.Callus == edited.Callus && d.Address == edited.Address
            && d.Callus != edited.Callus && d.Email != edited.Email && d.Address != edited.Address
            );
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate category name");
                return View();
            }
            contact.Callus = edited.Callus;
            contact.Email = edited.Email;
            contact.Address = edited.Address;


            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {

            if (id == 0) return NotFound();
            Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact is null) return NotFound();
            return View(contact);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact is null) return NotFound();
            return View(contact);
        }


        [HttpPost]
        public IActionResult Delete(int id, Contact delete)
        {
            if (id == 0) return NotFound();
            Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact is null) return NotFound();

            if (id == delete.Id)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(contact);
        }

    }
}
