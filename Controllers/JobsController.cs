using Microsoft.AspNetCore.Mvc;
using OrgChartApi.Interfaces;
using OrgChartApi.Data;

namespace OrgChartApi.Controllers {
  [Controller]
  [Route("api/[controller]")]
  public class JobsController {
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
  }
}