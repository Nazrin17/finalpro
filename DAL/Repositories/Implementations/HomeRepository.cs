
    public class HomeRepository:BaseRepository<Home,Hospital2DbContext>,IHomeRepository
    {
        private readonly Hospital2DbContext _context;

        public HomeRepository(Hospital2DbContext context):base(context)
        {
            _context = context;
        }
    }

