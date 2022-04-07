
using AlemedalGameStore.Utility;
using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using AlmedalGameStore.Models;
using AlmedalGameStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

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

    
    [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize]
        public IActionResult Details(Cart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            Cart cartFromDb = _unitOfWork.Cart.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


            if (cartFromDb == null)
            {

                _unitOfWork.Cart.Add(shoppingCart);
                
                //HttpContext.Session.SetInt32(SD.SessionCart,
                //    _unitOfWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
            else
            {
                _unitOfWork.Cart.IncrementCount(cartFromDb, shoppingCart.Count);
                //_unitOfWork.Save();
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
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