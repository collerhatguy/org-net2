using Microsoft.AspNetCore.Mvc;
using OrgChartApi.Data;
using OrgChartApi.Interfaces;

namespace OrgChartApi.Controllers {
  [Controller]
  [Route("api/[controller]")]
  public class DepartmentsController: ControllerBase {
    public readonly Database _context;
    public DepartmentsController(Database context) {
      _context = context;
    }

    [HttpGet]
    public List<Department> GetDepartments() {
      return _context.Departments.ToList();
    } 

    [HttpPost]
    public async Task<List<Department>> PostDepartments([FromBody] Department department) {
      _context.Departments.Add(department);
      await _context.SaveChangesAsync();
      return _context.Departments.ToList();
    } 
    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Department>>> DeleteDepartments(int id) {
      var department = await _context.Departments.FindAsync(id);
      if (department is null) {
        return NotFound();
      }
      _context.Departments.Remove(department);
      await _context.SaveChangesAsync();
      return _context.Departments.ToList();
    } 
  }
}