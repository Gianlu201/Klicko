using System.Security.Claims;
using Klicko_be.DTOs.Category;
using Klicko_be.DTOs.Experience;
using Klicko_be.DTOs.Order;
using Klicko_be.DTOs.OrderExperience;
using Klicko_be.DTOs.Voucher;
using Klicko_be.Models;
using Klicko_be.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly CouponService _couponService;
        private readonly FidelityCardService _fidelityCardService;

        public OrderController(
            OrderService orderService,
            ExperienceService experienceService,
            CouponService couponService,
            FidelityCardService fidelityCardService
        )
        {
            _orderService = orderService;
            _experienceService = experienceService;
            _couponService = couponService;
            _fidelityCardService = fidelityCardService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
                        SubTotalPrice = o.SubTotalPrice,
                        TotalDiscount = o.TotalDiscount,
                        ShippingPrice = o.ShippingPrice,
                        TotalPrice = o.TotalPrice,
                        CreatedAt = o.CreatedAt,
                        User = new DTOs.Account.UserSimpleDto()
                        {
                            UserId = o.User!.Id,
                            FirstName = o.User.FirstName,
                            LastName = o.User.LastName,
                            Email = o.User.Email,
                        },
                        OrderExperiences =
                            (o.OrderExperiences != null && o.OrderExperiences.Count > 0)
                                ? o
                                    .OrderExperiences.Select(oe => new OrderExperienceDto()
                                    {
                                        OrderExperienceId = oe.OrderExperienceId,
                                        Title = oe.Title,
                                        OrderId = oe.OrderId,
                                        UnitPrice = oe.TotalPrice,
                                        Quantity = oe.Quantity,
                                        TotalPrice = oe.TotalPrice,
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

        [HttpGet("getAllUserOrders")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> GetAllForUser()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user!.Value;

                var orders = await _orderService.GetAllOrdersForUserByIdAsync(userId);

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
                        SubTotalPrice = o.SubTotalPrice,
                        TotalDiscount = o.TotalDiscount,
                        ShippingPrice = o.ShippingPrice,
                        TotalPrice = o.TotalPrice,
                        CreatedAt = o.CreatedAt,
                        User = new DTOs.Account.UserSimpleDto()
                        {
                            UserId = o.User!.Id,
                            FirstName = o.User.FirstName,
                            LastName = o.User.LastName,
                            Email = o.User.Email,
                        },
                        OrderExperiences =
                            (o.OrderExperiences != null && o.OrderExperiences.Count > 0)
                                ? o
                                    .OrderExperiences.Select(oe => new OrderExperienceDto()
                                    {
                                        OrderExperienceId = oe.OrderExperienceId,
                                        Title = oe.Title,
                                        OrderId = oe.OrderId,
                                        UnitPrice = oe.TotalPrice,
                                        Quantity = oe.Quantity,
                                        TotalPrice = oe.TotalPrice,
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

        [HttpGet("getOrderById/{orderId:guid}")]
        [Authorize(Roles = "Admin, Seller, User")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    return Ok(
                        new GetOrderByIdResponseDto() { Message = "No orders found!", Order = null }
                    );
                }

                var orderDto = new OrderDto()
                {
                    OrderId = order.OrderId,
                    OrderNumber = order.OrderNumber,
                    UserId = order.UserId,
                    State = order.State,
                    SubTotalPrice = order.SubTotalPrice,
                    TotalDiscount = order.TotalDiscount,
                    ShippingPrice = order.ShippingPrice,
                    TotalPrice = order.TotalPrice,
                    CreatedAt = order.CreatedAt,
                    User = new DTOs.Account.UserSimpleDto()
                    {
                        UserId = order.User!.Id,
                        FirstName = order.User.FirstName,
                        LastName = order.User.LastName,
                        Email = order.User.Email,
                    },
                    OrderExperiences =
                        (order.OrderExperiences != null && order.OrderExperiences.Count > 0)
                            ? order
                                .OrderExperiences.Select(oe => new OrderExperienceDto()
                                {
                                    OrderExperienceId = oe.OrderExperienceId,
                                    Title = oe.Title,
                                    OrderId = oe.OrderId,
                                    UnitPrice = oe.TotalPrice,
                                    Quantity = oe.Quantity,
                                    TotalPrice = oe.TotalPrice,
                                })
                                .ToList()
                            : null,
                    Vouchers =
                        (order.Vouchers != null && order.Vouchers.Count > 0)
                            ? order
                                .Vouchers.Select(voucher => new VoucherDto()
                                {
                                    VoucherId = voucher.VoucherId,
                                    Title = voucher.Title,
                                    CategoryId = voucher.CategoryId,
                                    Duration = voucher.Duration,
                                    Place = voucher.Place,
                                    Price = voucher.Price,
                                    Organiser = voucher.Organiser,
                                    IsFreeCancellable = voucher.IsFreeCancellable,
                                    VoucherCode = voucher.VoucherCode,
                                    ReservationDate = voucher.ReservationDate,
                                    IsUsed = voucher.IsUsed,
                                    CreatedAt = voucher.CreatedAt,
                                    Category =
                                        voucher.Category != null
                                            ? (
                                                new CategorySimpleDto()
                                                {
                                                    CategoryId = voucher.Category.CategoryId,
                                                    Description = voucher.Category.Description,
                                                    Name = voucher.Category.Name,
                                                }
                                            )
                                            : null,
                                })
                                .ToList()
                            : null,
                };

                return Ok(
                    new GetOrderByIdResponseDto() { Message = "Order found!", Order = orderDto }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "User, Seller, Admin")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto createOrder)
        {
            try
            {
                decimal shippingPrice = 4.99m;

                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = user!.Value;

                if (userId == null)
                {
                    return BadRequest(
                        new CreateOrderResponseDto() { Message = "Something went wrong!" }
                    );
                }

                var fidelityCard = await _fidelityCardService.GetFidelityCardByUserIdAsync(userId);

                if (fidelityCard == null)
                {
                    return BadRequest(
                        new CreateOrderResponseDto() { Message = "Something went wrong!" }
                    );
                }

                if (fidelityCard.Points >= 1000)
                {
                    shippingPrice = 0;
                }

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

                var newOrderId = Guid.NewGuid();

                var newOrder = new Order()
                {
                    OrderId = newOrderId,
                    UserId = userId,
                    State = "In attesa",
                    ShippingPrice = shippingPrice,
                    CreatedAt = DateTime.UtcNow,
                    OrderExperiences = createOrder
                        .OrderExperiences.Select(oe =>
                        {
                            var exp = experiences.FirstOrDefault(e =>
                                e.ExperienceId == oe.ExperienceId
                            );

                            if (exp == null)
                            {
                                throw new Exception("Experience not found!");
                            }

                            return new OrderExperience()
                            {
                                OrderExperienceId = Guid.NewGuid(),
                                Title = exp.Title,
                                OrderId = newOrderId,
                                UnitPrice = ((exp.Price * (100 - exp.Sale)) / 100),
                                TotalPrice = ((exp.Price * (100 - exp.Sale)) / 100) * oe.Quantity,
                                Quantity = oe.Quantity,
                            };
                        })
                        .ToList(),
                };

                List<Voucher> vouchersList = [];

                foreach (var oe in createOrder.OrderExperiences)
                {
                    var exp = experiences.FirstOrDefault(e => e.ExperienceId == oe.ExperienceId);
                    if (exp == null)
                    {
                        throw new Exception("Experience not found!");
                    }
                    for (int i = 0; i < oe.Quantity; i++)
                    {
                        vouchersList.Add(
                            new Voucher()
                            {
                                VoucherId = Guid.NewGuid(),
                                Title = exp.Title,
                                CategoryId = exp.CategoryId,
                                Duration = exp.Duration,
                                Place = exp.Place,
                                Price = exp.Price * (1 - exp.Sale / 100),
                                Organiser = exp.Organiser,
                                IsFreeCancellable = exp.IsFreeCancellable,
                                ReservationDate = null,
                                IsUsed = false,
                                ExpirationDate = DateTime.UtcNow.AddMonths(exp.ValidityInMonths),
                                UserId = null,
                                CreatedAt = DateTime.UtcNow,
                                OrderId = newOrderId,
                            }
                        );
                    }
                }

                newOrder.Vouchers = vouchersList;

                newOrder.SubTotalPrice = newOrder.OrderExperiences.Sum(oe => oe.TotalPrice);

                if (createOrder.CouponId != null)
                {
                    var couponUsed = await _couponService.GetCouponByIdAsync(
                        (Guid)createOrder.CouponId
                    );

                    if (couponUsed != null)
                    {
                        if (couponUsed.PercentualSaleAmount > 0)
                        {
                            newOrder.TotalDiscount =
                                newOrder.SubTotalPrice * couponUsed.PercentualSaleAmount / 100;
                        }
                        else if (couponUsed.FixedSaleAmount > 0)
                        {
                            newOrder.TotalDiscount = (decimal)couponUsed.FixedSaleAmount;
                        }
                    }
                    else
                    {
                        throw new Exception("Coupon not found!");
                    }
                }
                else
                {
                    newOrder.TotalDiscount = 0;
                }

                newOrder.TotalPrice =
                    newOrder.SubTotalPrice - newOrder.TotalDiscount + newOrder.ShippingPrice;

                var result = await _orderService.CreateOrderAsync(newOrder, userId);

                if (result && createOrder.CouponId != null)
                {
                    await _couponService.UpdateCouponValidityByIdAsync(
                        (Guid)createOrder.CouponId,
                        false
                    );
                }

                if (!result)
                {
                    return BadRequest(
                        new CreateOrderResponseDto() { Message = "Something went wrong!" }
                    );
                }

                fidelityCard.Points += (int)newOrder.SubTotalPrice;
                fidelityCard.AvailablePoints += (int)newOrder.SubTotalPrice;

                await _orderService.TrySaveAsync();

                return Ok(
                    new CreateOrderResponseDto()
                    {
                        Message = "Order created successfully!",
                        OrderId = newOrderId,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("editOrderState/{orderId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditState(
            [FromBody] EditOrderStateRequestDto newOrderState,
            Guid orderId
        )
        {
            try
            {
                var options = new List<string>()
                {
                    "In attesa",
                    "Spedito",
                    "Completato",
                    "Cancellato",
                };

                if (!options.Contains(newOrderState.State))
                {
                    return BadRequest(
                        new EditOrderStateResponseDto() { Message = "Something went wrong!" }
                    );
                }

                var result = await _orderService.EditOrderStateAsync(orderId, newOrderState.State);

                return result
                    ? Ok(
                        new EditOrderStateResponseDto()
                        {
                            Message = "Order state updated successfully!",
                        }
                    )
                    : BadRequest(
                        new EditOrderStateResponseDto() { Message = "Something went wrong!" }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
