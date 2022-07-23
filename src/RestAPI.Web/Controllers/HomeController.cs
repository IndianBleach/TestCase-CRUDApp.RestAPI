using Microsoft.AspNetCore.Mvc;
using RestAPI.Application.DTOs.FilterDTOs;
using RestAPI.Application.DTOs.HomeDTOs;
using RestAPI.Application.Services.OrderItemService;
using RestAPI.Application.Services.OrderService;


namespace RestAPI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public HomeController(IOrderService orderService,
            IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index(SearchOrdersFilterDto filter)
        {
            HomeDto viewModel = new(
                await _orderService.GetOrdersAsync(filter),
                await _orderService.GetAllOrderProvidersAsync(),
                new SearchOrdersFilterDto(),
                await _orderItemService.GetAllDistinctUnitsAsync());

            return View(viewModel);
        }
    }
}