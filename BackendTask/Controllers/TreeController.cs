using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BackendTask.Controllers;

[ApiController]
public class TreeController : ControllerBase
{
    public TreeController()
    {
        
    }

    [Route("api.user.[controller].get")]
    [HttpPost]
    public async Task<IActionResult> Get()
    {
        return Ok("hello");
    }
}