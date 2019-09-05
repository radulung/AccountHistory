using AccountHistory.DataAccess.Models;
using System.Linq;

namespace AccountHistory.DataAccess.Queries
{
    public class TransactionQueries : BaseQuery
    {
        public TransactionQueries(AccountHistoryContext context) : base(context)
        {
        }

        public IQueryable<Transaction> GetByMonth(int month)
        {
            return Context.Transactions.Where(t => t.Date.Month == month);
        }

        public IQueryable<Transaction> GetOrderByDate()
        {
            return Context.Transactions.OrderByDescending(t => t.Date);
        }
    }
}
