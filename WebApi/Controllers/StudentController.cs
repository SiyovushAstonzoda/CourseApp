using Domain.Wrapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private StudentService _studentService;
    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }
  

    [HttpGet("GetAllstudents")]
     public async Task<Response<List<Student>>> GetAllStudents()
    {
        return await _studentService.GetAllStudents();
    }

    [HttpPost("AddStudent")]
    public async Task<Response<Student>> AddStudent(Student student)
    {
        return await _studentService.AddStudent(student);
    }

    [HttpPut("UpdateStudent")]
    public async Task<Response<Student>> UpdateStudent(Student student)
    {
        return await _studentService.UpdateStudent(student);
    }

    [HttpDelete("DeleteStudent")]
    public async Task<Response<string>> DeleteStudent(int id)
    {
        return await _studentService.DeleteStudent(id);
    }
}
