using Microsoft.EntityFrameworkCore;
using StudentAccountMangement.Modals;
using StudentAccountMangement.Modals.DTO;

namespace StudentAccountMangement
{
    public class SAMContext : DbContext
    {
        internal object RequestViewModel;

        public SAMContext(DbContextOptions<SAMContext> options) : base(options)
        {
        }

        public DbSet<User> User_tb { get; set; }
        public DbSet<Account> Account_tb { get; set; }
        public DbSet<Statement> Statement_tb { get; set; }
        public DbSet<Request> Request_tb { get; set; }
        public DbSet<Login> Login_tb { get; set; }
        public DbSet<LoginHistory> LoginHistory_tb { get; set; }

        public DbSet<StudentStatement> StudentStatements { get; set; }
        public DbSet<AdminStatement> AdminStatements { get; set; }
        public DbSet<StudentStatementById> StudentStatementByIds { get; set; }
        public DbSet<StudStatement> StudStatements { get; set; }  
        public DbSet<StudentTotalsById> StudentTotalsByIds { get; set; }
        public DbSet<AllStudents> allStudents { get; set; }
        public DbSet<RequestViewModel> requestViewModels { get; set; }
        public DbSet<GetAllStudents> getAllStudents { get; set; }
        public DbSet<StudentsTotals> StudentsTotal { get; set; }
        public DbSet<TotalAmount> totalAmounts { get; set; }
        public DbSet<RequestById> requestByIds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(e => e.Role).HasConversion<string>();
            modelBuilder.Entity<StudentStatement>().HasNoKey();
            modelBuilder.Entity<AdminStatement>().HasNoKey();
            modelBuilder.Entity<StudentStatementById>().HasNoKey();
            modelBuilder.Entity<StudStatement>().HasNoKey();
            modelBuilder.Entity<StudentTotalsById>().HasNoKey();
            modelBuilder.Entity<AllStudents>().HasNoKey();
            modelBuilder.Entity<RequestViewModel>().HasNoKey();
            modelBuilder.Entity<GetAllStudents>().HasNoKey();
            modelBuilder.Entity<StudentsTotals>().HasNoKey();
            modelBuilder.Entity<TotalAmount>().HasNoKey();
            modelBuilder.Entity<RequestById>().HasNoKey();
        }
    }
}