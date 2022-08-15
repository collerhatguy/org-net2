
namespace OrgChartApi.Interfaces {
  public class EmployeeDto {
    
    public string firstName { get; set; } = "";
    public string lastName { get; set; } = "";
    public char middleInitial { get; set; } = 'T';
    public bool isManager { get; set; } = false;
    public int departmentId { get; set; }
    public int jobId { get; set; }
    public int? managerId { get; set; }
  }
}