using Npgsql;
using Dapper;
using Domain.Wrapper;
using Domain.Entities;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class GroupService
{
     private DataContext.DataContext _context;
     public GroupService (DataContext.DataContext context)
    {
        _context = context;
    }

     public async Task<Response<List<Group>>> GetAllGroups()
    {
        await using var connection = _context.CreateConnection();
        var sql = "select  * from GroupOfStudents";
        var result = await connection.QueryAsync<Group>(sql);
        return new Response<List<Group>>(result.ToList());
    }

     public async Task<Response<Group>> AddGroup(Group group)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql = "insert into GroupOfStudents (GroupName, GroupDescription, CourseId) values (@GroupName, @GroupDescription, @CourseId) returning id;";
            var result = await connection.ExecuteScalarAsync<int>(sql, new { group.GroupName, group.GroupDescription, group.CourseId });
            group.Id = result;
            return new Response<Group>(group);
        }
        catch (Exception ex)
        {
            return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

      public async Task<Response<Group>> UpdateGroup(Group group)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"update GroupOfStudents set GroupName = @GroupName, GroupDescription = @GroupDescription, CourseId = @CourseId  where Id = @Id returning Id";
            var response  = await connection.ExecuteScalarAsync<int>(sql, new{group.GroupName, group.GroupDescription, group.CourseId, group.Id});
            group.Id = response;
            return new Response<Group>(group);
        }
        }
         catch (Exception e)
        {     
           return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }  
       
    }

    public async Task<Response<string>> DeleteGroup(int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"delete from GroupOfStudents where Id = {id}";
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
