using Microsoft.EntityFrameworkCore;
using OrgChartApi.Interfaces;

namespace OrgChartApi.Data {
  public class JobsRepo: DbContext {
    public JobsRepo(DbContextOptions<JobsRepo> options): base(options) {}
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Department> Departments { get; set; }
  }
}