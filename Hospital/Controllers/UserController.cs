using Domain.Models;
using Domain.UseCases;
using Hospital.Token;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("get_user")]
        public IActionResult GetUserByLogin(string login)
        {
            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "No username");

            var userRes = _service.GetUserByLogin(login);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Message);

            return Ok(userRes.Value);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(string username, string password, string phone_number, string fullname, Role role)
        {
            User user = new(0, phone_number, fullname, role, username, password);
            var register = _service.Register(user);

            if (register.IsFailure)
                return Problem(statusCode: 404, detail: register.Message);

            return Ok(new { access_token = TokenManager.GetToken(register.Value) });
        }

        [HttpGet("login")]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return Problem(statusCode: 404, detail: "Invalid login or password");

            var user = _service.GetUserByLogin(username);
            if (user.IsFailure)
                return Problem(statusCode: 404, detail: "Invalid login or password");

            if (!user.Value.Password.Equals(password))
                return Problem(statusCode: 404, detail: "Invalid login or password");

            return Ok(new { access_token = TokenManager.GetToken(user.Value) });
        }

        [HttpGet("is_user")]
        public IActionResult IsUserExists(string login)
        {
            var res = _service.UserExists(login);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [HttpGet("get_all")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll().Value);
        }
    }
}
