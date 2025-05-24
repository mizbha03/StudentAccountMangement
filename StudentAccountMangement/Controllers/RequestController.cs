using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentAccountMangement.Modals;
using StudentAccountMangement.Modals.DTO;

namespace StudentAccountMangement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly SAMContext _context;

        public RequestController(SAMContext context)
        {
            _context = context;
        }

        [HttpPost("SubmitRequest")]
        public async Task<IActionResult> SubmitRequest(StudentRequest studentRequest)
        {
            Request newRequest = new Request
            {
                User_id = studentRequest.User_id,
                Description = studentRequest.Description,
                Amount = studentRequest.Amount,
                Request_date = DateTime.Now,
                Proceeded_by = null,
                Proceeded_date = null,
                Status = "Pending"
            };

            _context.Request_tb.Add(newRequest);
            await _context.SaveChangesAsync();

            return Ok("Request submitted successfully.");
        }

        [HttpGet("GetAllPendingRequests")]
        public async Task<ActionResult<IEnumerable<Request>>> GetAllPendingRequests()
        {
            var requests = await _context.Request_tb.Where(r => r.Proceeded_by == null).ToListAsync();

            return Ok(requests);
        }

        [HttpPost("ApproveRequest/{requestId}/{adminId}")]
        public async Task<IActionResult> ApproveRequest(int requestId, int adminId)
        {
            try
            {
                var request = await _context.Request_tb.FirstOrDefaultAsync(r=> r.Request_id == requestId);
                if (request == null)
                {
                    return NotFound("Request not found.");
                }

                if (request.Amount == null)
                {
                    return BadRequest("Request amount is missing.");
                }

                var account = await _context.Account_tb.FirstOrDefaultAsync(a => a.User_id == request.User_id);
                if (account == null)
                {
                    account = new Account
                    {
                        User_id = request.User_id,
                        Balance = 0
                    };
                    _context.Account_tb.Add(account);
                    await _context.SaveChangesAsync();
                }

                if (account.Balance < request.Amount.Value)
                {
                    return BadRequest("Insufficient balance.");
                }

              
                account.Balance -= request.Amount.Value;
                _context.Account_tb.Update(account);

               
                request.Status = "Approved";
                request.Proceeded_by = adminId;
                request.Proceeded_date = DateTime.Now;
                _context.Request_tb.Update(request);

                var statement = new Statement
                {
                    User_id = request.User_id,
                    Type = "withdraw",
                    Amount = request.Amount.Value,
                    Mode = "Cash",
                    Status = "Approved",
                    Admin_id = adminId,
                    TDate = DateTime.Now
                };
                _context.Statement_tb.Add(statement);

                await _context.SaveChangesAsync();
                return Ok("Request approved and balance updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("RejectRequest/{requestId}/{adminId}")]
        public async Task<IActionResult> RejectRequest(int requestId, int adminId, [FromBody] Reason dto)
        {
            try
            {
                Request request = await _context.Request_tb.FindAsync(requestId);
                if (request == null) return NotFound();

                request.Status = "Rejected";
                request.Proceeded_by = adminId;
                request.Proceeded_date = DateTime.Now;
                request.RequestReason = dto.RejectionReason;


                _context.Request_tb.Update(request);
                await _context.SaveChangesAsync();

                return Ok("Request rejected.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var results = await _context.requestViewModels.FromSqlRaw("EXEC GetAllRequests").ToListAsync();

            return Ok(results);
        }

        [HttpGet("GetRequestsByUserId/{userId}")]
        public async Task<IActionResult> GetRequestsByUserId(int userId)
        {
            var param = new SqlParameter("@UserId", userId);

            var results = await _context.requestByIds.FromSqlRaw("EXEC GetRequestsByUserId @UserId", param).ToListAsync();

            return Ok(results);
        }
    }
}