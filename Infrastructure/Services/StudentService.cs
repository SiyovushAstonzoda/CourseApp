using Npgsql;
using Dapper;
using Domain.Wrapper;
using Domain.Entities;
using Infrastructure.DataContext;
namespace Infrastructure.Services;

public class StudentService
{
    private DataContext.DataContext _context;
     public StudentService (DataContext.DataContext context)
    {
        _context = context;
    }

     public async Task<Response<List<Student>>> GetAllStudents()
    {
        await using var connection = _context.CreateConnection();
        var sql = "select  * from Student";
        var result = await connection.QueryAsync<Student>(sql);
        return new Response<List<Student>>(result.ToList());
    }

     public async Task<Response<Student>> AddStudent(Student student)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql = "insert into Student (FirstName, LastName, Email, Phone, Address, City) values (@FirstName, @LastName, @Email, @Phone, @Address, @City) returning id;";
            var result = await connection.ExecuteScalarAsync<int>(sql, new { student.FirstName, student.LastName, student.Email, student.Phone, student.Address, student.City});
            student.Id = result;
            return new Response<Student>(student);
        }
        catch (Exception ex)
        {
            return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

      public async Task<Response<Student>> UpdateStudent(Student student)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"update Student set FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Address = @Address, City = @City where Id = @Id returning Id";
            var response  = await connection.ExecuteScalarAsync<int>(sql, new{student.FirstName, student.LastName, student.Email, student.Phone, student.Address, student.City, student.Id});
            student.Id = response;
            return new Response<Student>(student);
        }
        }
         catch (Exception e)
        {     
           return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }  
       
    }

    public async Task<Response<string>> DeleteStudent(int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Student where Id = {id}";
            var response  = await connection.ExecuteScalarAsync<int>(sql);
            id = response;
            return new Response<string>("Success");
        }
        }
         catch (Exception e)
        {
           return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    } 
}
