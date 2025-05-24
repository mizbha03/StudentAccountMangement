using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountMangement.Modals;

namespace StudentAccountMangement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SAMContext _context;

        public AccountController(SAMContext context)
        {
            _context = context;
        }

        [HttpGet("GetBalancebyId/{userId}")]
        public async Task<IActionResult> GetBalance(int userId)
        {
            var account = await _context.Account_tb.FirstOrDefaultAsync(a => a.User_id == userId);
            if (account == null)
                return NotFound("Account not found");

            return Ok(account.Balance);
        }

        [HttpGet("GetAllBalance")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllStatements()
        {
            List<Account> account = await _context.Account_tb.ToListAsync();
            return account;
        }
    }
}
