using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace AlmedalGameStoreWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Visa orderbekräftelse.
        public IActionResult Index()
        {
            return View();
        }
    }
}
