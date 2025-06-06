using AGEX.CORE.Dtos.Orders.Delete;
using AGEX.CORE.Dtos.Orders.Get;
using AGEX.CORE.Dtos.Orders.Register;
using AGEX.CORE.Dtos.Orders.Update;
using AGEX.CORE.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AGEX.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route(nameof(RegisterOrder)), HttpPost]
        public async Task<IActionResult> RegisterOrder([FromBody] RegisterOrderReqDto request)
        {
            var dt = await _orderService.RegisterOrder(request);

            return Ok(dt);
        }

        [Route(nameof(UpdateOrder)), HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderReqDto request)
        {
            var dt = await _orderService.UpdateOrder(request);

            return Ok(dt);
        }

        [Route(nameof(GetOrder)), HttpPost]
        public async Task<IActionResult> GetOrder([FromBody] GetOrdersReqDto request)
        {
            var dt = await _orderService.GetOrder(request);

            return Ok(dt);
        }

        [Route(nameof(DeleteOrder)), HttpPost]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderReqDto request)
        {
            var dt = await _orderService.DeleteOrder(request);

            return Ok(dt);
        }
    }
}
