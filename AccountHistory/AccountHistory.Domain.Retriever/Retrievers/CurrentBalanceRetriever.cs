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
    public class CurrentBalanceRetriever : IRetriever<double>
    {
        private readonly TransactionQueries _transactionQueries;

        public CurrentBalanceRetriever(TransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }

        public async Task<double> GetAsync()
        {
            var lastTransaction = await GetLastTransaction();
            return lastTransaction != null ? lastTransaction.Balance : 0;
        }

        private async Task<Transaction> GetLastTransaction()
        {
            return await _transactionQueries.GetOrderByDate().FirstOrDefaultAsync();
        }
    }
}
