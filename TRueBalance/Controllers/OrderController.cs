using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRueBalance.Data.Entities;
using TRueBalance.Models.OrderViewModels;
using TRueBalance.Repositories.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository _orderRepository)
        {
            this.orderRepository = _orderRepository;
        }

        public int GetLastID()
        {
            return orderRepository.GetLastID();
        }

        public IActionResult Screen()
        {
            OrderViewModel model = new OrderViewModel()
            {
                OrderList = orderRepository.GetList
            };

            return View(model);
        }

        public IActionResult List()
        {
            OrderViewModel model = new OrderViewModel()
            {
                OrderList = orderRepository.GetList
            };

            return View(model);
        }

        
        public IActionResult RemoveAddFromQueue(int idOrder)
        {
            orderRepository.RemoveAddFromQueue(idOrder);
            return RedirectToAction("List");
        }

        [HttpPost]
        public void RemoveAddFromQueue([FromBody] OrderViewModel model)
        {
            orderRepository.RemoveAddFromQueue(model.OrderID);
        }

        public IActionResult Delete(int idOrder)
        {
            orderRepository.Delete(idOrder);
            return RedirectToAction("List");
        }
    }
}
