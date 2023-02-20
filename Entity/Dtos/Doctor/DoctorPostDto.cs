
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Http;

public class DoctorPostDto
{
    public string Name { get; set; }
    public string About { get; set; }
    public string Email { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public bool IsDeleted { get; set; }
    public IFormFile formFile { get; set; }
    public int PositionId { get; set; }
    public Position? Position { get; set; }
    public List<Icon>? Icons { get; set; }
    public List<string> Urls { get; set; }
    public List<string> Tags { get; set; }
    public List<ResHistory>? history { get; set; }

    public List<Rezerv>? rezervs { get; set; }
    public List<string> Times { get; set; }

}

