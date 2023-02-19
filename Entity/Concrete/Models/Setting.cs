
    public class Setting
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? SecTitle { get; set; }
        public string Logo { get; set; }
        public string? LogoIcon { get; set; }
        public List<SettingIcon> Icons { get; set; }    
    }

