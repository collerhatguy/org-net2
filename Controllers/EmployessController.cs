using Microsoft.AspNetCore.Mvc;
using OrgChartApi.Data;
using OrgChartApi.Interfaces;

namespace OrgChartApi.Controllers {
  [Controller]
  [Route("api/[controller]")]
  public class EmployeesController : ControllerBase {
    public readonly Database context;
    public EmployeesController(Database db) {
      context = db;
    }
    [HttpGet]
    public ActionResult<List<Employee>> GetEmployees() {
      return Ok(context.Employees.ToList());
    }
    // public EmployeeDto PostEmployee([FromBody] EmployeeDto body) {
    [HttpPost]
    public async Task<ActionResult<List<Employee>>> PostEmployee([FromBody] EmployeeDto body) {
      if (body is null) {
        return NotFound("bad request");
      }
      var job = await context.Jobs.FindAsync(body.jobId);
      if (job is null) {
        return NotFound("Job does not exist");
      }
      var department = await context.Departments.FindAsync(body.departmentId);
      if (department is null) {
        return NotFound("Department does not exist");
      }
      return GetEmployees();
    }
  }
}