using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Application.ApiResultModel;
using RestAPI.Application.DTOs.OrderItemDTOs;
using RestAPI.Application.Services.OrderItemService;
using RestAPI.Application.Services.OrderService;

namespace RestAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;

        public ItemController(IOrderItemService orderItemService)
            => _orderItemService = orderItemService;

        [HttpDelete("{itemId:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute]int itemId)
            => Ok(ApiResult<bool>.SuccessOk(await _orderItemService
                .DeleteOrderItemAsync(itemId)));

        [HttpPut("{itemId:int}")]
        public async Task<IActionResult> PutItem([FromRoute]int itemId, [FromForm] UpdateOrderItemDto model)
            => Ok(ApiResult<bool>.SuccessOk(await _orderItemService
                .UpdateOrderItemAsync(itemId, model)));

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromForm]CreateOrderItemDto model)
        {
            if (ModelState.IsValid)
                return Ok(ApiResult<bool>.SuccessOk(await _orderItemService
                .CreateOrderItemAsync(model)));
            
            return BadRequest(ApiResult<bool>
                .Failed(false, new List<string>() { "Input is incorrect" }));            
        }
    }
}
