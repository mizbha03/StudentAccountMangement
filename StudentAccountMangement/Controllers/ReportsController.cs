using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountMangement.Helper;
using StudentAccountMangement.Modals.DTO;

namespace StudentAccountMangement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly SAMContext _context;

        public ReportsController(SAMContext context)
        {
            _context = context;
        }

        [HttpGet("GetStudentStatements")]
        public async Task<ActionResult<IEnumerable<StudentStatement>>> GetStudentStatements()
        {
            var result = await _context.StudentStatements.FromSqlRaw("EXEC GetStudentStatements").ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetStudentStatementsById/{userId}")]
        public async Task<ActionResult<IEnumerable<StudentStatementById>>> GetStudentStatementsById(int userId)
        {
            var result = await _context.StudentStatementByIds.FromSqlRaw("EXEC GetStudentStatementById @UserId = {0}", userId).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetAdminStatements")]
        public async Task<ActionResult<IEnumerable<AdminStatement>>> GetAdminStatements()
        {
            var result = await _context.AdminStatements.FromSqlRaw("EXEC GetAdminStatements").ToListAsync();

            return Ok(result);
        }

        [HttpGet("StudentStatementById/{userId}")]
        public async Task<ActionResult<IEnumerable<StudStatement>>> StudentStatementById(int userId)
        {
            var result = await _context.StudStatements.FromSqlRaw("EXEC StudentStatementById @UserId = {0}", userId).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetStudentTotalsById/{userId}")]
        public async Task<ActionResult<IEnumerable<StudentTotalsById>>> GetStudentTotalsById(int userId)
        {
            var result = await _context.StudentTotalsByIds.FromSqlRaw("EXEC GetStudentTotalsById @UserId ={0}", userId).ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetStudentTotals")]
        public async Task<ActionResult<IEnumerable<StudentsTotals>>> GetStudentTotals()
        {
            var result = await _context.StudentsTotal.FromSqlRaw("EXEC GetStudentTotals").ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetTotalAmount")]
        public async Task<ActionResult<IEnumerable<TotalAmount>>> GetTotalAmount()
        {
            var result = await _context.totalAmounts
                .FromSqlRaw("EXEC GetTotalAmount").ToListAsync();

            return Ok(result);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] NewStudentByStaff stud)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddStudentsByStaff @Name = {0}, @UserName = {1}, @Password = {2}, @ProfileImage = {3}",
                stud.Name, stud.UserName, PasswordHelper.Encrypt(stud.Password), stud.ProfileImage
            );

            return Ok("Student added successfully." );
        }

        [HttpDelete("DeleteStudent/{User_id}")]
        public async Task<IActionResult> DeleteStudent(int User_id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC DeleteStudentById @User_id = {0}", User_id
            );

            return Ok("Student deleted successfully.");
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudents stud)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateStudentById @User_id = {0}, @Name = {1}, @UserName = {2}, @ProfileImage = {3}",
                stud.User_id, stud.Name, stud.UserName, (object)stud.ProfileImage ?? DBNull.Value
            );

            return Ok("Student updated successfully.");
        }

        [HttpGet("AllStudents")]
        public async Task<ActionResult<IEnumerable<GetAllStudents>>> AllStudents()
        {
            var result = await _context.getAllStudents.FromSqlRaw("EXEC AllStudents").ToListAsync();

            return Ok(result);
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<IEnumerable<AllStudents>>> GetAllStudents()
        {
            var result = await _context.allStudents.FromSqlRaw("EXEC GetAllStudents").ToListAsync();

            return Ok(result);
        }
    }
}
