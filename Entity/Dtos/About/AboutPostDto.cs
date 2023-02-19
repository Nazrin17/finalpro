
using Microsoft.AspNetCore.Http;

public class AboutPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile formFile { get; set; }
        public List<Button> Buttons { get; set; }
    }
