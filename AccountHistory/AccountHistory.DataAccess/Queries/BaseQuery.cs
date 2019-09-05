namespace AccountHistory.DataAccess.Queries
{
    public class BaseQuery : IQuery
    {
        private readonly AccountHistoryContext _context;

        protected BaseQuery(AccountHistoryContext context)
        {
            _context = context;
        }

        protected AccountHistoryContext Context => _context;
    }
}
