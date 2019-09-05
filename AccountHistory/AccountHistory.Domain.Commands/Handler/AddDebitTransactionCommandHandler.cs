using AccountHistory.Core;
using AccountHistory.DataAccess;
using AccountHistory.DataAccess.Models;
using AccountHistory.DataAccess.Queries;
using AccountHistory.Domain.Commands.InputOptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AccountHistory.Common.Domain.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace AccountHistory.Domain.Commands.Handler
{
    public class AddDebitTransactionCommandHandler : ICommand<AddTransactionInputOptions, int?>
    {
        private readonly AccountHistoryContext _context;
        private readonly TransactionQueries _transactionQueries;

        public AddDebitTransactionCommandHandler(AccountHistoryContext context,
            TransactionQueries transactionQueries)
        {
            _context = context;
            _transactionQueries = transactionQueries;
        }

        public async Task<int?> ExecuteAsync(AddTransactionInputOptions addTransactionInputOptions)
        {
            Guard.ArgumentNotNull(addTransactionInputOptions, nameof(addTransactionInputOptions));

            var lastTransaction = await GetLastTransaction();
            if (lastTransaction != null)
            {
                ValidateAmount(addTransactionInputOptions.Amount, lastTransaction.Balance);
            }

            var lastBalance = lastTransaction != null ? lastTransaction.Balance : 0;
            return await AddTransactionAsync(addTransactionInputOptions, lastBalance);
        }

        private bool ValidateAmount(double amount, double currentBalance)
        {
            if (currentBalance < amount)
            {
                throw new InvalidTransactionException();
            }

            return true;
        }

        private async Task<Transaction> GetLastTransaction()
        {
            return await _transactionQueries.GetOrderByDate().FirstOrDefaultAsync();
        }

        private async Task<int?> AddTransactionAsync(AddTransactionInputOptions addTransactionInputOptions, double lastBalance)
        {
            var newTransaction = new Transaction
            {
                Amount = addTransactionInputOptions.Amount,
                OperationId = addTransactionInputOptions.OperationId,
                Date = DateTime.Now,
                Balance = lastBalance - addTransactionInputOptions.Amount
            };

            _context.Transactions.Add(newTransaction);
            if (await _context.SaveChangesAsync() > 0)
            {
                return newTransaction.Id;
            }

            return null;
        }
    }
}
