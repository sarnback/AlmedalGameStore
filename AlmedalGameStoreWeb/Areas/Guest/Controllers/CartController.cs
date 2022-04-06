using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace AlmedalGameStoreWeb.Areas.Guest.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get JSON product objects per product ID
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
    }
}
