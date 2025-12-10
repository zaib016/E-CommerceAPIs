using E_CommerceAPIs.Models;
using E_CommerceAPIs.Models.Entities;
using E_CommerceAPIs.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepo = orderRepository;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add(OrderDTOs orderDTOs)
        {
            var order = new Order
            {
                UserId = orderDTOs.UserId,
                Status = orderDTOs.Status,
                Price = orderDTOs.Price,
            };

            await _orderRepo.AddAsync(order);
            return Ok("Order Added Successfully");
        }
        [HttpGet("user/{UserId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrderUserById(int UserId)
        {
            var order = await _orderRepo.GetOrderUserByIdAsync(UserId);
            if (order == null) return NotFound();

            return Ok(order);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

    }
}
