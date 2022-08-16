using Microsoft.AspNetCore.Mvc;
using OrgChartApi.Data;
using OrgChartApi.Interfaces;
using Microsoft.EntityFrameworkCore;

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
      var employees = context.Employees
        .Include(x => x.department)
        .Include(x => x.manager)
        .Include(x => x.job);
      return Ok(employees);
    }
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
      Employee newEmp = new Employee();
      if (body.managerId != null) {
        var manager = await context.Employees.FindAsync(body.managerId);
        if (department is null) {
          return NotFound("Manager does not exist");
        }
        manager.isManager = true;
        newEmp.manager = manager;
      }
      newEmp.firstName = body.firstName;
      newEmp.lastName = body.lastName;
      newEmp.middleInitial = body.middleInitial;
      newEmp.isManager = body.isManager;
      newEmp.departmentId = body.departmentId;
      newEmp.jobId = body.jobId;
      newEmp.department = department;
      newEmp.job = job;
      await context.Employees.AddAsync(newEmp);
      await context.SaveChangesAsync();
      return GetEmployees();
    }
  }
}