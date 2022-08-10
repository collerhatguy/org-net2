using Microsoft.AspNetCore.Mvc;
using OrgChartApi.Interfaces;
using OrgChartApi.Data;

namespace OrgChartApi.Controllers {
  [Controller]
  [Route("api/[controller]")]
  public class JobsController: ControllerBase {
    public readonly JobsRepo _repo;
    public JobsController(JobsRepo repo) {
      _repo = repo;
    }

    [HttpGet]
    public List<Job> GetJobs() {
      return  _repo.Jobs.ToList();
    }

    [HttpPost]
    public async Task<List<Job>> PostJob([FromBody]Job job) {
      _repo.Jobs.Add(job);
      await _repo.SaveChangesAsync();
      return _repo.Jobs.ToList();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Job>>> DeleteJob(int id) {
      var task = _repo.Jobs.Find(id);
      if (task is null) {
        return NotFound();
      }
      _repo.Jobs.Remove(task);
      await _repo.SaveChangesAsync();
      return _repo.Jobs.ToList();
    }
    [HttpPut]
    public async Task<ActionResult<List<Job>>> UpdateJob([FromBody] Job job) {
      var task = await _repo.Jobs.FindAsync(job.id);
      if (task is null) {
        return NotFound();
      }
      task.name = job.name;
      await _repo.SaveChangesAsync();
      return GetJobs();
    }
  }
}