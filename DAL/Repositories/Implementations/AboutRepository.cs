
    public class AboutRepository:BaseRepository<About,Hospital2DbContext>,IAboutRepository
    {
        private readonly Hospital2DbContext _context;

        public AboutRepository(Hospital2DbContext context):base(context)
        {
            _context = context;
        }
    }

