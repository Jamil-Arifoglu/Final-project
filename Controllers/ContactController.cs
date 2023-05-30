//using Gaming.DAL;
//using Gaming.Entities;
//using Gaming.ViewModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace Gaming.Controllers
//{
//    public class ContactController : Controller
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly GamingDbContext _dbContext;

//        public ContactController(UserManager<User> userManager, SignInManager<User> signInManager, GamingDbContext dbContext)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _dbContext = dbContext;
//        }

//        public IActionResult Contact()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Contact(ContactVM model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Yeni bir Contact nesnesi oluştur
//                var User = new User
//                {
//                    FirstName = model.FirstName,
//                    LastName = model.LastName,
//                    Email = model.Email,

//                    Description = model.Description,
//                    CreationTime = DateTime.Now
//                };

//                // Contact nesnesini veritabanına kaydet
//                _dbContext.Contacts.Add(contact);
//                _dbContext.SaveChanges();

//                // İletişim formu başarıyla gönderildiyse, teşekkür sayfasına yönlendir
//                return RedirectToAction("ThankYou");
//            }

//            // Model geçerli değilse, formu aynı sayfada tekrar göster ve hataları görüntüle
//            return View(model);
//        }

//        public IActionResult ThankYou()
//        {
//            return View();
//        }
//    }
//}
