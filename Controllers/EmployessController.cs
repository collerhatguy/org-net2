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
    [HttpPost]
    public async Task<ActionResult<List<Employee>>> PostEmployee([FromBody] EmployeeDto body) {
      var job = await context.Jobs.FindAsync(body.jobId);
      if (job is null) {
        return NotFound();
      }
      return GetEmployees();
    }
  }
}