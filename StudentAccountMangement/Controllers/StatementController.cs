using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountMangement.Modals;

namespace StudentAccountMangement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatementController : ControllerBase
    {
        private readonly SAMContext _context;

        public StatementController(SAMContext context)
        {
            _context = context;
        }

        [HttpGet("GetStatement/{T_id}")]
        public async Task<ActionResult<Statement>> GetSatement(int T_id)
        {
            Statement Statement = await _context.Statement_tb.FirstOrDefaultAsync(x => x.T_id == T_id);

            if (Statement == null)
            {
                return NotFound();
            }
            else
            {
                return Statement;
            }
        }

        [HttpGet("GetAllStatements")]
        public async Task<ActionResult<IEnumerable<Statement>>> GetAllStatements()
        {
            List<Statement> statements = await _context.Statement_tb.ToListAsync();
            return statements;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Statement statement)
        {
            try
            {
                if (statement.Amount <= 0)
                {
                    return BadRequest("Amount must be greater than zero.");
                }

                var account = await _context.Account_tb.FirstOrDefaultAsync(a => a.User_id == statement.User_id);
                if (account == null)
                {
                    account = new Account
                    {
                        User_id = statement.User_id,
                        Balance = 0
                    };

                    _context.Account_tb.Add(account);
                    await _context.SaveChangesAsync();
                }

                _context.Statement_tb.Add(statement);
                await _context.SaveChangesAsync();

                if (statement.Type.ToLower() == "deposit")
                {
                    account.Balance += statement.Amount;
                }

                else if (statement.Type.ToLower() == "withdraw")
                {
                    if (account.Balance <= 0)
                    {
                        return BadRequest("Account balance is zero. Cannot withdraw.");
                    }

                    if (account.Balance < statement.Amount)
                    {
                        return BadRequest("Insufficient balance.");
                    }
                    account.Balance -= statement.Amount;
                }

                _context.Account_tb.Update(account);
                await _context.SaveChangesAsync();

                return Ok("Transaction Saved and Balance Updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("Update")]
        public async Task<ActionResult<User>> Update(Statement Statement)
        {
            try
            {
                Statement Existstatement = await _context.Statement_tb.FirstOrDefaultAsync(x => x.T_id == Statement.T_id);

                if (Existstatement != null)
                {
                    Existstatement.User_id = Statement.User_id;
                    Existstatement.Type = Statement.Type;
                    Existstatement.Amount = Statement.Amount;
                    Existstatement.Mode = Statement.Mode;
                    Existstatement.Status = Statement.Status;
                    Existstatement.Admin_id = Statement.Admin_id;
                    Existstatement.TDate = Statement.TDate;

                    _context.Statement_tb.Update(Existstatement);
                    await _context.SaveChangesAsync();

                    return Ok(Existstatement);
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

        [HttpDelete("Delete/{T_id}")]
        public async Task<IActionResult> Delete(int T_id)
            {
            try
            {
                Statement Existstatement = await _context.Statement_tb.FirstOrDefaultAsync(x => x.T_id == T_id);
                if (Existstatement == null)
                {
                    return NotFound();
                }

                _context.Statement_tb.Remove(Existstatement);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}