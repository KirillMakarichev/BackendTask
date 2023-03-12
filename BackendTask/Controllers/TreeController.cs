﻿using AutoMapper;
using BackendTask.Models.Entities;
using BackendTask.Providers.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BackendTask.Controllers;

[ApiController]
public class TreeController : ControllerBase
{
    private readonly ITreeProvider _treeProvider;
    private readonly IMapper _mapper;

    public TreeController(IServiceProvider serviceProvider)
    {
        _treeProvider = serviceProvider.GetService<ITreeProvider>();
        _mapper = serviceProvider.GetService<IMapper>();
    }

    [Route("api.user.[controller].get")]
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

        return added ? Ok() : NotFound();
    }
}