
    public class MessageRepository:BaseRepository<Message,Hospital2DbContext>,IMessageRepository
    {
        private readonly Hospital2DbContext _context;

        public MessageRepository(Hospital2DbContext context):base(context) 
        {
            _context = context;
        }
    }

