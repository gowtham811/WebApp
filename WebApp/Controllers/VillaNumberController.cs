using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Entites;
using WebApp.Infrastructure.Data;
using WebApp.Web.ViewModel;

namespace WebApp.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var VillaNumbers = _db.VillaNumbers.Include(u=>u.Villa).ToList();
            return View(VillaNumbers);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList=_db.Villas.ToList().Select(u=> new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                )
            };
            
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM Obj)
        {
            //ModelState.Remove("Villa");
            bool isNumUni=_db.VillaNumbers.Any(u=>u.Villa_Number==Obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !isNumUni)
            {
                _db.VillaNumbers.Add(Obj.VillaNumber );
                _db.SaveChanges();
                TempData["success"] = "The villa Number has been successfully.";
                return RedirectToAction("Index");
            }
            if (isNumUni)
            {
                TempData["error"] = "The villa Number is already Exists!";
            }
            Obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }
             );
            return View(Obj);

        }
        public IActionResult Update(int villaNumberId)
        {
            //Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            //Villa? obj = _db.Villas.Find(villaId);
            //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
               ),
                VillaNumber=_db.VillaNumbers.FirstOrDefault(u=>u.Villa_Number== villaNumberId)

            };
            if (villaNumberVM.VillaNumber==null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }


        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
