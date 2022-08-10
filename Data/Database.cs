using Microsoft.EntityFrameworkCore;
using OrgChartApi.Interfaces;

namespace OrgChartApi.Data {
  public class Database: DbContext {
    public Database(DbContextOptions<Database> options): base(options) {}
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Department> Departments { get; set; }
  }
}