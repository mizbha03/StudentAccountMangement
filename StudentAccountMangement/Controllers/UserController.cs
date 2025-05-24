using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountMangement.Helper;
using StudentAccountMangement.Modals;
using StudentAccountMangement.Modals.DTO;

namespace StudentAccountMangement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SAMContext _context;

        public UserController(SAMContext context)
        {
            _context = context;
        }

        [HttpGet("GetUser/{User_id}")]
        public async Task<ActionResult<User>> GetUser(int User_id)
        {
            try
            {
                User user = await _context.User_tb.FirstOrDefaultAsync(x => x.User_id == User_id);

                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            List<User> user = await _context.User_tb.ToListAsync();
            return user;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.ProfileImage) && !string.IsNullOrEmpty(user.ImagePath))
                {
                    user.ImagePath = ImagePathHelper.SaveFile(user.ProfileImage, user.ImagePath);
                }

                user.ProfileImage = "";

                _context.User_tb.Add(user);
                await _context.SaveChangesAsync();

                return Ok("User Saved!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<User>> Update(User user)
        {
            try
            {
                User ExistUser = await _context.User_tb.FirstOrDefaultAsync(x => x.User_id == user.User_id);

                if (ExistUser != null)
                {
                    ExistUser.Name = user.Name;
                    if (!string.IsNullOrEmpty(user.ProfileImage))
                    {
                        ExistUser.ImagePath = ImagePathHelper.SaveFile(user.ProfileImage, ".png");
                    }
                    ExistUser.UserName = user.UserName;
                    ExistUser.Password = user.Password; 
                    ExistUser.Role = user.Role;

                    _context.User_tb.Update(ExistUser);
                    await _context.SaveChangesAsync();

                    return Ok(ExistUser);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{User_id}")]
        public async Task<IActionResult> DeleteCourse(int User_id)
        {
            User ExistUser = await _context.User_tb.FirstOrDefaultAsync(x => x.User_id == User_id);
            if (ExistUser == null)
            {
                return NotFound();
            }

            _context.User_tb.Remove(ExistUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var user = await _context.User_tb.FirstOrDefaultAsync(x => x.User_id == request.UserId);

                if (user == null)
                    return NotFound("User not found.");

           
                if (user.Password.Trim() != request.CurrentPassword.Trim())
                    return Unauthorized("Current password is incorrect.");

                user.Password = request.NewPassword.Trim();
                _context.User_tb.Update(user);
                await _context.SaveChangesAsync();

                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}