using AutoMapper;
using BackendTask.Models.Routs.Requests;
using BackendTask.Models.Routs.Responses;
using BackendTask.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendTask.Controllers;

[ApiController]
public class JournalController : ControllerBase
{
    private readonly IExceptionsProvider _exceptionsProvider;
    private readonly IMapper _mapper;

    public JournalController(IServiceProvider serviceProvider)
    {
        _exceptionsProvider = serviceProvider.GetRequiredService<IExceptionsProvider>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    [HttpGet]
    [Route("/api.user.[controller].getRange")]
    public async Task<IActionResult> Get([FromQuery] int skip, [FromQuery] int take,
        [FromBody] JournalGetRequest filter)
    {
        var (count, exceptions) = await _exceptionsProvider.GetExceptionsAsync(take, skip, filter);

        return Ok(new { skip, count, items = _mapper.Map<List<ExceptionResponse>>(exceptions) });
    }
    
    [HttpGet]
    [Route("/api.user.[controller].getSingle")]
    public async Task<IActionResult> Get([FromQuery] long id)
    {
        var exceptionData = await _exceptionsProvider.GetExceptionAsync(id);

        return Ok(exceptionData);
    }
}