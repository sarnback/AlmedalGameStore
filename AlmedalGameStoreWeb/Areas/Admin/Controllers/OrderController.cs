using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using AlmedalGameStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AlemedalGameStore.Utility;

namespace AlmedalGameStoreWeb.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _hostEnviorment;

        public OrderController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviorment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ListOrder = _unitOfWork.Order.GetAll(o => o.OrderId == id, includeProperties: "Product");

            if (ListOrder == null)
            {
                return NotFound();
            }
            
            return View(ListOrder.ToList());
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var orderList = _unitOfWork.Order.GetAll().AsQueryable().DistinctBy(o => o.OrderId);
            return Json(new { data = orderList });
        }

        #endregion

    }
}
