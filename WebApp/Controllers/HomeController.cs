using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Common.Interfaces;
using WebApp.Models;
using WebApp.Web.ViewModel;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll(includeProperties:"VillaAmenity"),
                Nights=1,
                CheckInDate=DateOnly.FromDateTime(DateTime.Now),

            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]*/
        public IActionResult Error()
        {
            /* return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });*/
            return View();
        }
    }
}
