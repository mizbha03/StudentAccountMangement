using Microsoft.AspNetCore.Mvc;
using StudentAccountMangement.Helper;
using StudentAccountMangement.Modals.DTO;

namespace StudentAccountMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase  
    {
        private readonly SAMContext _context;
             
        public AuthController(SAMContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.User_tb.FirstOrDefault(u => u.UserName.ToLower() == request.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid User");
            }

            string pass = PasswordHelper.Decrypt(user.Password);

            if (pass != request.Password)
            {
                return Unauthorized("Invalid Password");
            }

            string role = user.Role.ToString();
            string token = AuthHelper.GenerateJwtToken(request.Username, role, user.User_id); 

            return Ok(new
            {
                Token = token,
                Username = user.UserName,
                Role = role,
                UserId = user.User_id
              
            });
        }

        [HttpGet("GetServerStatus")]
        public ActionResult GetServerStatus()
        {
            return Ok("Server running successful!");
        }
    }
}
 