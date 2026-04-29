using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        public OrderController(OrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequest order)
        {
            var result = await _service.CreateOrder(order);
            return Ok(result);
        }
    }
}
