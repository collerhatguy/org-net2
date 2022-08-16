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
        .Where(x => x.isActive)
        .Include(x => x.department)
        .Include(x => x.manager)
        .Include(x => x.job);
      return Ok(employees);
    }
    [HttpGet]
    [Route("managers")]
    public ActionResult<List<Employee>> GetManagers() {
      var employees = context.Employees
        .Where(x => x.isActive)
        .Where(x => x.isManager)
        .Include(x => x.department)
        .Include(x => x.manager)
        .Include(x => x.job);
      return Ok(employees);
    }
    [HttpGet]
    [Route("no-managers")]
    public ActionResult<List<Employee>> GetNoManagers() {
      var employees = context.Employees
        .Where(x => x.isActive)
        .Where(x => x.managerId == null)
        .Where(x => x.isManager)
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
        newEmp.managerId = body.managerId;
      }
      newEmp.firstName = body.firstName;
      newEmp.lastName = body.lastName;
      newEmp.middleInitial = body.middleInitial;
      newEmp.isManager = body.isManager;
      newEmp.departmentId = body.departmentId;
      newEmp.jobId = body.jobId;
      await context.Employees.AddAsync(newEmp);
      await context.SaveChangesAsync();
      return GetEmployees();
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id) {
      var employee = await context.Employees.FindAsync(id);
      if (employee is null) {
        return NotFound();
      }
      employee.isActive = false;
      await context.SaveChangesAsync();
      return GetEmployees();
    }
    [HttpPut]
    public async Task<ActionResult<List<Employee>>> UpdateEmployee([FromBody] EmployeeDto body) {
      var employee = await context.Employees.FindAsync(body.id);
      if (employee is null) {
        return NotFound();
      }
      employee.firstName = body.firstName;
      employee.lastName = body.lastName;
      employee.middleInitial = body.middleInitial;
      employee.isManager = body.isManager;
      employee.departmentId = body.departmentId;
      employee.jobId = body.jobId;
      if (body.managerId != null) {
        employee.managerId = body.managerId;
      }
      await context.SaveChangesAsync();
      return GetEmployees();
    }
  }
}