
    public class DoctorRepository:BaseRepository<Doctor,Hospital2DbContext>,IDoctorRepository
    {
        private readonly Hospital2DbContext _context;

        public DoctorRepository(Hospital2DbContext context):base(context)
        {
            _context = context;
        }
    }

