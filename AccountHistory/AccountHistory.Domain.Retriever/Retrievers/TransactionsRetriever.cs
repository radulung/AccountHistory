using AccountHistory.Common.Domain.Dtos;
using AccountHistory.Core;
using AccountHistory.DataAccess.Models;
using AccountHistory.DataAccess.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountHistory.Domain.Retriever.Retrievers
{
    public class TransactionsRetriever : IRetriever<int, IEnumerable<TransactionDto>>
    {
        private readonly TransactionQueries _transactionQueries;

        public TransactionsRetriever(TransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        public async Task<IEnumerable<TransactionDto>> GetAsync(int month)
        {
            return await _transactionQueries.GetByMonth(month)
                .Include(t => t.Operation)
                .OrderBy(t => t.Date)
                .Select(t => MapToDto(t))
                .ToListAsync();
        }

        private TransactionDto MapToDto(Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Balance = transaction.Balance,
                Date = transaction.Date,
                Operation = transaction.Operation.Name
            };
        }
    }
}
