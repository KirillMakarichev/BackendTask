using AutoMapper;
using BackendTask.Extensions;
using BackendTask.Models.Entities;
using BackendTask.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Exception = BackendTask.DataBase.Models.Exception;
using TreeNode = BackendTask.Models.Entities.TreeNode;

namespace BackendTask.Controllers;

[ApiController]
public class TreeController : ControllerBase
{
    private readonly ITreeProvider _treeProvider;
    private readonly IMapper _mapper;
    private readonly IExceptionsProvider _exceptionsProvider;

    public TreeController(IServiceProvider serviceProvider)
    {
        _treeProvider = serviceProvider.GetRequiredService<ITreeProvider>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _exceptionsProvider = serviceProvider.GetRequiredService<IExceptionsProvider>();
    }

    [Route("/api.user.[controller].get")]
    [HttpPost]
    public async Task<IActionResult> Get([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest();

        var tree = await _treeProvider.GetOrCreateAsync(name);

        var mappedTree = _mapper.Map<TreeNode>(tree);

        return Ok(mappedTree);
    }

    [Route("/api.user.[controller].node.create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] string treeName,
        [FromQuery] long parentNodeId,
        [FromQuery] string nodeName)
    {
        var added = await _treeProvider.CreateNodeAsync(treeName, parentNodeId, nodeName);

        var loggedRequest = await LogRequestAsync(added);

        return ReturnResult(loggedRequest);
    }

    [Route("/api.user.[controller].node.rename")]
    [HttpPost]
    public async Task<IActionResult> Rename([FromQuery] string treeName,
        [FromQuery] long nodeId,
        [FromQuery] string newNodeName)
    {
        var renamed = await _treeProvider.RenameNodeAsync(treeName, nodeId, newNodeName);

        var loggedRequest = await LogRequestAsync(renamed);

        return ReturnResult(loggedRequest);
    }

    [Route("/api.user.[controller].node.delete")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string treeName,
        [FromQuery] long nodeId)
    {
        var deleted = await _treeProvider.DeleteNodeAsync(treeName, nodeId);
        if (deleted.Success)
            return Ok();

        var loggedRequest = await LogRequestAsync(deleted);

        return ReturnResult(loggedRequest);
    }

    private IActionResult ReturnResult(Exception exception) =>
        StatusCode(500, new
        {
            type = ExceptionType.Secure.ToString(),
            id = exception.Id,
            data = new
            {
                message = exception.Data.Message
            }
        });

    private async Task<Exception> LogRequestAsync(ProcessingResponse response)
    {
        var data = await HttpContext.CastToExceptionData(response.Message);

        return await _exceptionsProvider.LogExceptionAsync(ExceptionType.Secure, data);
    }
}