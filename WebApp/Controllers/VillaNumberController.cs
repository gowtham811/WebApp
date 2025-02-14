﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Application.Common.Interfaces;
using WebApp.Domain.Entites;
using WebApp.Infrastructure.Data;
using WebApp.Web.ViewModel;

namespace WebApp.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var VillaNumbers = _unitOfWork.villaNumber.GetAll(includeProperties: "Villa");
            return View(VillaNumbers);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList=_unitOfWork.Villa.GetAll().Select(u=> new SelectListItem
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
            bool isNumUni=_unitOfWork.villaNumber.Any(u=>u.Villa_Number==Obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !isNumUni)
            {
                _unitOfWork.villaNumber.Add(Obj.VillaNumber );
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been successfully.";
                return RedirectToAction(nameof(Index));
            }
            if (isNumUni)
            {
                TempData["error"] = "The villa Number is already Exists!";
            }
            Obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
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
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
               ),
                VillaNumber=_unitOfWork.villaNumber.Get(u=>u.Villa_Number== villaNumberId)

            };
            if (villaNumberVM.VillaNumber==null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            //bool isNumUni = _db.VillaNumbers.Any(u => u.Villa_Number == Obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid)
            {
                _unitOfWork.villaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been successfully.";
                return RedirectToAction(nameof(Index));
            }
            
            villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }
             );

            /*if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been updated successfully!";
                return RedirectToAction("Index");
            }*/
            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            /*Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);*/
            //Villa? obj = _db.Villas.Find(villaId);
            //var VillaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 0);
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
               ),
                VillaNumber = _unitOfWork.villaNumber.Get(u => u.Villa_Number == villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }


        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? objFromDb = _unitOfWork.villaNumber
                .Get(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            if (objFromDb is not null)
            {
                _unitOfWork.villaNumber.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa number  has been deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The villa number could not be deleted";
            return View();
        }
    }
}
