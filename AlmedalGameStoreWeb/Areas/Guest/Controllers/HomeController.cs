
using AlemedalGameStore.Utility;
using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using AlmedalGameStore.Models;
using AlmedalGameStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Genre");

            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            Cart cartObj = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.GetFirstOrDefault
                (u => u.Id == productId, includeProperties: "Genre")
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
            }
            else
            {
                _unitOfWork.Cart.IncrementCount(cartFromDb, shoppingCart.Count);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
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

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}