
using Entities.Concrete.Models;

public class DoctorGetDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? About { get; set; }
    public string? Image { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool? IsDeleted { get; set; }
    public int? PositionId { get; set; }
    public Position? Position { get; set; }
    public List<ResHistory>? history { get; set; }
    public List<Icon>? Icons { get; set; }
    public List<Rezerv>? rezervs { get; set; }
}

