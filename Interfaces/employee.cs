
namespace OrgChartApi.Interfaces {
  public class Employee {
    public int id { get; init; }
    public string firstName { get; set; } = "";
    public string lastName { get; set; } = "";
    public char middleInitial { get; set; } = 'T';
    public bool isManager { get; set; } = false;
    public int departmentId { get; set; }
    public Department department { get; set; }
    public int jobId { get; set; }
    public Job job { get; set; }
    public int managerId { get; set; }
    public Employee manager { get; set; }
  }
}