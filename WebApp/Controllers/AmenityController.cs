using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Common.Interfaces;
using WebApp.Domain.Entites;
using WebApp.Infrastructure.Data;
using WebApp.Web.ViewModel;

namespace WebApp.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var Amenitys = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(Amenitys);
        }
        public IActionResult Create()
        {
            AmenityVM AmenityVM = new()
            {
                VillaList=_unitOfWork.Villa.GetAll().Select(u=> new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                )
            };
            
            return View(AmenityVM);
        }
        [HttpPost]
        public IActionResult Create(AmenityVM Obj)
        {
            //ModelState.Remove("Villa");
            //bool isNumUni=_unitOfWork.Amenity.Any(u=>u.Villa_Number==Obj.Amenity.Villa_Number);

            if (ModelState.IsValid )
            {
                _unitOfWork.Amenity.Add(Obj.Amenity );
                _unitOfWork.Save();
                TempData["success"] = "The Amenity Number has been successfully.";
                return RedirectToAction(nameof(Index));
            }
            
            Obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }
             );
            return View(Obj);

        }
        public IActionResult Update(int AmenityId)
        {
            //Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            //Villa? obj = _db.Villas.Find(villaId);
            //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);
            AmenityVM AmenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
               ),
                Amenity=_unitOfWork.Amenity.Get(u=>u.Id == AmenityId)

            };
            if (AmenityVM.Amenity==null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(AmenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM AmenityVM)
        {
            //bool isNumUni = _db.Amenitys.Any(u => u.Villa_Number == Obj.Amenity.Villa_Number);

            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(AmenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The Amenity Number has been successfully.";
                return RedirectToAction(nameof(Index));
            }
            
            AmenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }
             );

            /*if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The Amenity has been updated successfully!";
                return RedirectToAction("Index");
            }*/
            return View(AmenityVM);
        }

        public IActionResult Delete(int AmenityId)
        {
            /*Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);*/
            //Villa? obj = _db.Villas.Find(villaId);
            //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);
            AmenityVM AmenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
               ),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == AmenityId)

            };
            if (AmenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(AmenityVM);
        }


        [HttpPost]
        public IActionResult Delete(AmenityVM AmenityVM)
        {
            Amenity? objFromDb = _unitOfWork.Amenity
                .Get(u => u.Id == AmenityVM.Amenity.Id);
            if (objFromDb is not null)
            {
                _unitOfWork.Amenity.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The Amenity  has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The Amenity could not be deleted";
            return View();
        }
    }
}
