using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using AlmedalGameStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlmedalGameStoreWeb.Areas.Guest.Controllers
{

    [Area("Guest")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartVM CartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //CartVM = new CartVM()
            //{
            //    ListCart = _unitOfWork.Cart.GetAll(
            //        u => u.ApplicationUser == claim.Value, includeProperties: "Product")
            //};

            foreach (var cart in CartVM.ListCart)
            {
                cart.Price = GetPrice(cart.Count, cart.Product.Price);
                CartVM.Order.OrderTotal += (cart.Price * cart.Count);
            }
            return View(CartVM);
        }
        private double GetPrice(double quantity, double price)
        {
            return price;
        }
    }
}
