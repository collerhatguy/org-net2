
namespace OrgChartApi.Interfaces {
  public class EmployeeDto {
    public int? id { get; set; }
    public string firstName { get; set; } = "";
    public string lastName { get; set; } = "";
    public char middleInitial { get; set; } = 'T';
    public bool isManager { get; set; } = false;
    public bool isActive { get; set; } = true;
    public int departmentId { get; set; }
    public int jobId { get; set; }
    public int? managerId { get; set; }
  }
}