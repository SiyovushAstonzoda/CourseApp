using Domain.Wrapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private GroupService _groupService;
    public GroupController(GroupService groupService)
    {
        _groupService = groupService;
    }
  

    [HttpGet("GetAllGroups")]
    public async Task<Response<List<Group>>> GetAllGroups()
    {
        return await _groupService.GetAllGroups();
    }

    [HttpPost("AddGroup")]
   public async Task<Response<Group>> AddGroup(Group group)
    {
        return await _groupService.AddGroup(group);
    }

    [HttpPut("UpdateGroup")]
    public async Task<Response<Group>> UpdateGroup(Group group)
    {
        return await _groupService.UpdateGroup(group);
    }

    [HttpDelete("DeleteGroup")]
     public async Task<Response<string>> DeleteGroup(int id)
    {
        return await _groupService.DeleteGroup(id);
    }
}
