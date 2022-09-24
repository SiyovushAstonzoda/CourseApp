using Domain.Wrapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MentorController : ControllerBase
{
    private MentorService _mentorService;
    public MentorController(MentorService mentorService)
    {
        _mentorService = mentorService;
    }
  

    [HttpGet("GetAllMentors")]
    public async Task<Response<List<Mentor>>> GetAllMentors()
    {
        return await _mentorService.GetAllMentors();
    }

    [HttpPost("AddMentor")]
   public async Task<Response<Mentor>> AddMentor(Mentor mentor)
    {
        return await _mentorService.AddMentor(mentor);
    }

    [HttpPut("UpdateMentor")]
    public async Task<Response<Mentor>> UpdateMentor(Mentor mentor)
    {
        return await _mentorService.UpdateMentor(mentor);
    }

    [HttpDelete("DeleteMentor")]
     public async Task<Response<string>> DeleteMentor(int id)
    {
        return await _mentorService.DeleteMentor(id);
    }
}
