using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Application.ApiResultModel;
using RestAPI.Application.DTOs.OrderDTOs;
using RestAPI.Application.DTOs.OrderItemDTOs;
using RestAPI.Application.Services.OrderItemService;
using RestAPI.Application.Services.OrderService;

namespace RestAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
            => _orderService = orderService;

        [HttpPost]        
        public async Task<IActionResult> CreateOrder([FromForm]CreateOrderDto model)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<string>.SuccessOk(await _orderService.CreateAsync(model)));
            
            else return BadRequest(ApiResult<string>
                        .Failed(null, new List<string>() { "Input is incorrect" }));           
        }

        [HttpDelete("{orderId:int}")]
        public async Task<IActionResult> DeleteOrder([FromRoute]int orderId)
            => Ok(ApiResult<bool>
                .SuccessOk(await _orderService.DeleteAsync(orderId)));

        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetOrderDetail([FromRoute]int orderId)
            => Ok(ApiResult<OrderDetailDto>
                .SuccessOk(await _orderService.GetOrderDetailOrNullAsync(orderId)));

        [HttpPut("{orderId:int}")]
        public async Task<IActionResult> PutOrder([FromRoute]int orderId, [FromForm] UpdateOrderDto model)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<bool>.SuccessOk(await _orderService.UpdateAsync(orderId, model)));

            else return BadRequest(ApiResult<bool>
                .Failed(false, new List<string>() { "Not found", "Something went wrong" }));
        }
    }
}
