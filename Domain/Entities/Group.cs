namespace Domain.Entities;

public class Group
{
    public string? GroupName { get; set; }
    public string? GroupDescription { get; set; }
    public int CourseId { get; set; }
    public int Id { get; set; }
}
