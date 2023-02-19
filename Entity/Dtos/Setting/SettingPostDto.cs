
    public class SettingPostDto
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? SecTitle { get; set; }
        public string Logo { get; set; }
        public string? LogoIcon { get; set; }
        public List<SettingIcon>? Icons { get; set; }
        public List<string>? Tags { get; set; }
        public List<string>? Urls { get; set; }
    }

