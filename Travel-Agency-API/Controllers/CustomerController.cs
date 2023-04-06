using Microsoft.AspNetCore.Mvc;
using System.Net;
using Travel_Agency_API.Entities;
using Travel_Agency_API.Models;

namespace Travel_Agency_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly EasyTravelAgencyProgramContext dbContext;
        public CustomerController(EasyTravelAgencyProgramContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpPost("AddCustomerInfo")]
        public async Task<ApiResponse> AddCustomerInfo(CustomerDTO customer) {
            try {
                var userID = (dbContext.Users?.FirstOrDefault(x => x.UserLogin == customer.UserLogin)?.UserId) ?? throw new Exception($"User with \"{customer.UserLogin}\" doesn't exists!");
                var newCustomerInfo = new Customer {
                    UserId = userID,
                    CustomerName = customer.CustomerName,
                    CustomerSurname = customer.CustomerSurname,
                    CustomerBirthdate = customer.CustomerBirthdate,
                    CustomerCountry = customer.CustomerCountry,
                };

                dbContext.Customers.Add(newCustomerInfo);
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message, ex.InnerException?.Message);
            }
            return new ApiResponse(HttpStatusCode.OK);
        }

    }
}
