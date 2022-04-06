
using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using AlmedalGameStore.Models;
using AlmedalGameStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlmedalGameStoreWeb.Controllers
{
    
    [Area("Guest")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        //Vi ska hämta alla produkter i en IEbumerable lista och skicka dem till vyn
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties:"Genre");

            return View(productList);

        }
        //hämtar id från asp-route-id i index view på produkt man interaktar med
        public IActionResult Details(int id)
        {
            Cart cartObj = new()
            {
                Count = 1,
                //för att kunna lägga till en produkt i kundkorg
                Product = _unitOfWork.Product.GetFirstOrDefault
                (u => u.Id == id, includeProperties: "Genre")
            };
        //returna cartObjektet till vyn
            return View(cartObj);

        }

        public IActionResult Checkout()
        {
            // Hämta array med tillagda produkter från den andra Jonas
            // och skicka vidare det på något sätt.

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}