using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Travel_Agency_API.Entities;
using Travel_Agency_API.Models;
using Travel_Agency_API.Utils;

namespace Art_Critique_Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly EasyTravelAgencyProgramContext dbContext;
        public UserController(EasyTravelAgencyProgramContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpPost("RegisterUser")]
        public async Task<ApiResponse> RegisterUser(UserDTO User) {
            try {
                var newUser = new User() {
                    UserLogin = User.UserLogin,
                    UserPassword = Encryptor.EncryptString(User.UserPassword),
                    UserIsAdmin = User.UserIsAdmin
                };

                dbContext.Users.Add(newUser);
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message, ex.InnerException?.Message);
            }
            return new ApiResponse(HttpStatusCode.OK);
        }
        [HttpGet("Login")]
        public async Task<ApiResponse> Login(string login, string password) {
            try {
                var encryptedPassword = Encryptor.EncryptString(password);
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserLogin == login);
                if (user?.UserPassword == encryptedPassword) {
                    return new ApiResponse(HttpStatusCode.OK);
                } else {
                    return new ApiResponse(HttpStatusCode.NoContent, "Invalid login or password!");
                }
            } catch (Exception ex) {
                return new ApiResponse(HttpStatusCode.BadRequest, ex.Message, ex.InnerException?.Message);
            }
        }
    }
}