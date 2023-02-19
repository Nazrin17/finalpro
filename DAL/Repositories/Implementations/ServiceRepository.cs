
    public class ServiceRepository:BaseRepository<Service,Hospital2DbContext>,IServiceRepository
    {
        private readonly Hospital2DbContext _context;

        public ServiceRepository(Hospital2DbContext context):base(context) 
        {
            _context = context;
        }
    }

