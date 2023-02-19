
using Microsoft.AspNetCore.Http;

public class HomePostDto
    {
        public string Title { get; set; }
        public string Slogan { get; set; }
        public IFormFile formFile{ get; set; }
    }

