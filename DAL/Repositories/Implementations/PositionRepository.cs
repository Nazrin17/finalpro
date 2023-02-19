
    public class PositionRepository:BaseRepository<Position,Hospital2DbContext>,IPositionRepository
    {
        private readonly Hospital2DbContext _context;

        public PositionRepository(Hospital2DbContext context):base(context) 
        {
            _context = context;
        }
    }

