
    public class SettingRepository:BaseRepository<Setting,Hospital2DbContext>,ISettingRepository
    {
        private readonly Hospital2DbContext _context;

        public SettingRepository(Hospital2DbContext context):base(context)
        {
            _context = context;
        }
    }

