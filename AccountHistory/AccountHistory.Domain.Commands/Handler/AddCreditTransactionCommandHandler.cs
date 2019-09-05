using AccountHistory.Common.Domain.Exceptions;
using AccountHistory.Core;
using AccountHistory.DataAccess;
using AccountHistory.DataAccess.Models;
using AccountHistory.DataAccess.Queries;
using AccountHistory.Domain.Commands.InputOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using System;
using System.Threading.Tasks;

namespace AccountHistory.Domain.Commands.Handler
{
    public class AddCreditTransactionCommandHandler : ICommand<AddTransactionInputOptions, int?>
    {
        private readonly AccountHistoryContext _context;
        private readonly TransactionQueries _transactionQueries;

        public AddCreditTransactionCommandHandler(AccountHistoryContext context,
            TransactionQueries transactionQueries)
        {
            _context = context;
            _transactionQueries = transactionQueries;
        }

        public async Task<int?> ExecuteAsync(AddTransactionInputOptions addTransactionInputOptions)
        {
            Guard.ArgumentNotNull(addTransactionInputOptions, nameof(addTransactionInputOptions));

            await ValidateAmount(addTransactionInputOptions.Amount);

             var lastBalance = await GetLastBalance();
            return await AddTransactionAsync(addTransactionInputOptions, lastBalance);
        }

        private async Task<bool> ValidateAmount(double amount)
        {
            if (amount > 1000)
            {
                throw new InvalidTransactionException();
            }

            var amountThisMonth = await GetAmountThisMonth();
            if (amountThisMonth + amount > 5000)
            {
                throw new InvalidTransactionException();
            }

            return true;
        }

        private async Task<double> GetLastBalance()
        {
            var lastTransaction = await _transactionQueries.GetOrderByDate().FirstOrDefaultAsync();

            return lastTransaction != null ? lastTransaction.Balance : 0;
        }

        private async Task<double> GetAmountThisMonth()
        {
            var amountThisMonth = await _transactionQueries.GetByMonth(DateTime.Now.Month).SumAsync(t => t.Amount);

            return amountThisMonth;
        }

        private async Task<int?> AddTransactionAsync(AddTransactionInputOptions addTransactionInputOptions, double lastBalance)
        {
            var newTransaction = new Transaction
            {
                Amount = addTransactionInputOptions.Amount,
                OperationId = addTransactionInputOptions.OperationId,
                Date = DateTime.Now,
                Balance = lastBalance + addTransactionInputOptions.Amount
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
