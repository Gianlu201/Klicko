using Klicko_be.DTOs.Category;
using Klicko_be.DTOs.Experience;
using Klicko_be.DTOs.Order;
using Klicko_be.Models;
using Klicko_be.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Klicko_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ExperienceService _experienceService;

        public OrderController(OrderService orderService, ExperienceService experienceService)
        {
            _orderService = orderService;
            _experienceService = experienceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();

                if (orders == null)
                {
                    return Ok(
                        new GetOrdersListResponseDto()
                        {
                            Message = "No orders found!",
                            Orders = null,
                        }
                    );
                }

                var ordersDto = orders
                    .Select(o => new OrderDto()
                    {
                        OrderId = o.OrderId,
                        OrderNumber = o.OrderNumber,
                        UserId = o.UserId,
                        State = o.State,
                        TotalPrice = o.TotalPrice,
                        CreatedAt = o.CreatedAt,
                        User = new DTOs.Account.UserSimpleDto()
                        {
                            UserId = o.User!.Id,
                            FirstName = o.User.FirstName,
                            LastName = o.User.LastName,
                            Email = o.User.Email,
                        },
                        Experiences =
                            (o.OrderExperiences != null && o.OrderExperiences.Count > 0)
                                ? o
                                    .OrderExperiences.Select(oe => new ExperienceForOrdersDto()
                                    {
                                        ExperienceId = oe.Experience!.ExperienceId,
                                        Title = oe.Experience.Title,
                                        CategoryId = oe.Experience.CategoryId,
                                        Duration = oe.Experience.Duration,
                                        Place = oe.Experience.Place,
                                        Price = oe.Experience.Price,
                                        Quantity = oe.Quantity,
                                        DescriptionShort = oe.Experience.DescriptionShort,
                                        MaxParticipants = oe.Experience.MaxParticipants,
                                        Organiser = oe.Experience.Organiser,
                                        CoverImage = oe.Experience.CoverImage,
                                        ValidityInMonths = oe.Experience.ValidityInMonths,
                                        Category =
                                            oe.Experience.Category != null
                                                ? new CategorySimpleDto()
                                                {
                                                    CategoryId = oe.Experience.Category.CategoryId,
                                                    Name = oe.Experience.Category.Name,
                                                    Description = oe.Experience
                                                        .Category
                                                        .Description,
                                                    Image = oe.Experience.Category.Image,
                                                    Icon = oe.Experience.Category.Icon,
                                                }
                                                : null,
                                    })
                                    .ToList()
                                : null,
                    })
                    .ToList();

                return Ok(
                    new GetOrdersListResponseDto()
                    {
                        Message = $"{ordersDto.Count} orders found!",
                        Orders = ordersDto,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto createOrder)
        {
            try
            {
                var experiences = await _experienceService.GetAllExperienceAsync();

                if (experiences == null)
                {
                    return StatusCode(500, "Impossible to reach experiences!");
                }

                if (createOrder.OrderExperiences == null || createOrder.OrderExperiences.Count == 0)
                {
                    return BadRequest(
                        new CreateOrderResponseDto() { Message = "Something went wrong!" }
                    );
                }

                var totalPrice = createOrder.OrderExperiences.Sum(oe =>
                    experiences.First(e => e.ExperienceId == oe.ExperienceId).Price * oe.Quantity
                );

                var newOrderId = Guid.NewGuid();

                // TODO associare automaticamente l'id dell'utente che fa l'ordine
                var newOrder = new Order()
                {
                    OrderId = newOrderId,
                    UserId = "70b579dd-c6a0-4075-8d7d-1326f2353c7b",
                    State = "In attesa",
                    TotalPrice = totalPrice,
                    CreatedAt = DateTime.UtcNow,
                    OrderExperiences = createOrder
                        .OrderExperiences.Select(oe => new OrderExperience()
                        {
                            OrderExperienceId = Guid.NewGuid(),
                            ExperienceId = oe.ExperienceId,
                            OrderId = newOrderId,
                            Quantity = oe.Quantity,
                        })
                        .ToList(),
                };

                var result = await _orderService.CreateOrderAsync(newOrder);

                return result
                    ? Ok(new CreateOrderResponseDto() { Message = "Order created successfully!" })
                    : BadRequest(
                        new CreateOrderResponseDto() { Message = "Something went wrong!" }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
