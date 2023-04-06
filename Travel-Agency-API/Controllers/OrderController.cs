using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using Travel_Agency_API.Entities;
using Travel_Agency_API.Models;

namespace Travel_Agency_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase {
        private readonly EasyTravelAgencyProgramContext dbContext;
        public OrderController(EasyTravelAgencyProgramContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpPost("CreateOrder")]
        public async Task<ApiResponse> CreateOrder(OrderDTO order) {
            try {
                if (order.UserLogin == null) {
                    throw new Exception("Order has no owner");
                }
                var userID = dbContext.Users.FirstOrDefault(x => x.UserLogin == order.UserLogin)?.UserId;
                var customerID = dbContext.Customers.FirstOrDefault(x => x.UserId == userID)?.CustomerId ?? 0;
                if (customerID == 0) {
                    throw new Exception("Error occured while getting a customer id of user");
                }

                var newOrder = new Order() {
                    CustomerId = customerID,
                    OrdersCity = order.OrdersCity,
                    OrdersCountry = order.OrdersCountry,
                    OrdersLengthDays = order.OrdersLengthDays,
                    OrdersDescription = order.OrdersDescription,
                };

                dbContext.Orders.Add(newOrder);
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message, ex.InnerException?.Message);
            }
            return new ApiResponse(HttpStatusCode.OK);
        }

        [HttpDelete("DeleteOrder")]
        public async Task<ApiResponse> DeleteOrder(int orderID) {
            try {
                var order = dbContext.Orders.FirstOrDefault(x => x.OrdersId == orderID) ?? throw new Exception("Order doesn't exists!");
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message, ex.InnerException?.Message);
            }
            return new ApiResponse(HttpStatusCode.OK);
        }

        [HttpPut("UpdateOrder")]
        public async Task<ApiResponse> UpdateOrder(UpdateOrderDTO newOrder) {
            try {
                var order = dbContext.Orders.FirstOrDefault(x => x.OrdersId == newOrder.OrdersId) ?? throw new Exception("Order doesn't exists!");
                order.OrdersCountry = newOrder.OrdersCountry;
                order.OrdersCity = newOrder.OrdersCity;
                order.OrdersLengthDays = newOrder.OrdersLengthDays;
                order.OrdersDescription = newOrder.OrdersDescription;
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message, ex.InnerException?.Message);
            }
            return new ApiResponse(HttpStatusCode.OK);
        }

        [HttpGet("ShowOrders")]
        public List<OrderDTO> GetOrders() {
            var list = dbContext.Orders.Select(x => new OrderDTO {
                OrdersCountry = x.OrdersCountry,
                OrdersCity = x.OrdersCity,
                OrdersLengthDays = x.OrdersLengthDays,
                OrdersDescription = x.OrdersDescription
            }).ToList();
            return list;
        }

        [HttpGet("ShowOrdersWithCustomerInfo")]
        public List<OrderCustomerInfoDTO> ShowOrdersWithCustomerInfo() {
            var orders = (from order in dbContext.Orders
                          join customer in dbContext.Customers on order.CustomerId equals customer.CustomerId
                          select new OrderCustomerInfoDTO {
                              OrdersId = order.OrdersId,
                              OrdersCountry = order.OrdersCountry,
                              OrdersCity = order.OrdersCity,
                              OrdersLengthDays = order.OrdersLengthDays,
                              OrdersDescription = order.OrdersDescription,
                              CustomerId = order.CustomerId,
                              CustomerName = customer.CustomerName ?? "Not found",
                              CustomerSurname = customer.CustomerSurname ?? "Not found",
                              CustomerCountry = customer.CustomerCountry ?? "Not found",
                              CustomerBirthdate = customer.CustomerBirthdate ?? DateTime.Now
                          }).ToList();
            return orders;
        }
    }
}
