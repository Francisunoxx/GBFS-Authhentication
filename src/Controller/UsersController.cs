using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyContext _myContext;
        private UserService _userService;
        private EmailSender _emailSender;

        public UsersController(MyContext myContext, UserService userService, EmailSender emailSender)
        {
            _userService = userService;
            _emailSender = emailSender;
            _myContext = myContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Authenticate authenticate)
        {
            var user = await _userService.Authenticate(authenticate.Email, authenticate.Password);

            HttpContext.Response.Cookies.Append(
            "Refresh-Token",
            user.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true
            });

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("forgot-password-request")]
        public async Task<IActionResult> ForgotPasswordRequest([FromBody] ForgotPasswordForm request)
        {
            var user = await _userService.GetByEmail(request.Email);

            if (user == null)
                return BadRequest(new { message = "Email does not exist " });

            bool isAdmin = (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3);

            await _emailSender.SendResetPassword(isAdmin, user);

            return Ok(user);

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordForm request)
        {
            var user = await _userService.GetByResetPwToken(request.token);

            if (user == null)
                return BadRequest(new { message = "Token is invalid " });

            var userPassEntity = await _myContext.UsrPassword.FirstOrDefaultAsync(_ => _.UserPasswordId == user.PasswordId);

            userPassEntity.Password = request.password;

            _myContext.UsrPassword.Update(userPassEntity);
            await _myContext.SaveChangesAsync();

            return Ok("ok");

        }

        [HttpPost("verify-reset-pw-token")]
        public async Task<IActionResult> VerifyResetPwToken([FromBody] VerifyResetPwTokenForm request)
        {
            var user = await _userService.GetByResetPwToken(request.token);

            if (user == null)
                return BadRequest(new { message = "Token is invalid " });

            return Ok("ok");

        }

        [HttpPost("authenticate-borrower")]
        public async Task<IActionResult> AuthenticateBorrower([FromBody] Authenticate authenticate)
        {
            var user = await _userService.AuthenticateBorrower(authenticate.Email, authenticate.Password);

            HttpContext.Response.Cookies.Append(
            "Refresh-Token",
            user.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true
            });

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }


        [Authorize]
        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var token = Request.Cookies["Refresh-Token"];
            var user = await _userService.Authenticate(token);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid" });
            }

            return Ok(user);
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            await _emailSender.Test();
            return Ok("");
        }
    }
}
