using AccountHistory.Common.Domain.Dtos;
using AccountHistory.Common.Domain.Enums;
using AccountHistory.Core.Exceptions;
using AccountHistory.Domain.Commands.Handler;
using AccountHistory.Domain.Commands.InputOptions;
using AccountHistory.Domain.Retriever.Retrievers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountHistory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountHistoryController : ControllerBase
    {
        private readonly AddCreditTransactionCommandHandler _addCreditTransactionCommandHandler;
        private readonly AddDebitTransactionCommandHandler _addDebitTransactionCommandHandler;
        private readonly TransactionsRetriever _transactionsRetriever;
        private readonly CurrentBalanceRetriever _currentBalanceRetriever;

        public AccountHistoryController(AddCreditTransactionCommandHandler addCreditTransactionCommandHandler,
            AddDebitTransactionCommandHandler addDebitTransactionCommandHandler,
            TransactionsRetriever transactionsRetriever,
            CurrentBalanceRetriever currentBalanceRetriever)
        {
            _addCreditTransactionCommandHandler = addCreditTransactionCommandHandler;
            _addDebitTransactionCommandHandler = addDebitTransactionCommandHandler;
            _transactionsRetriever = transactionsRetriever;
            _currentBalanceRetriever = currentBalanceRetriever;
        }

        // GET api/accountHistory/month
        [HttpGet("month/{month}")]
        public async Task<IEnumerable<TransactionDto>> Get(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new WrongRequestException();
            }

            return await _transactionsRetriever.GetAsync(month);
        }

        // GET api/accountHistory/currentBalance
        [HttpGet("{currentBalance}")]
        public async Task<double> GetCurrentBalance()
        {
            return await _currentBalanceRetriever.GetAsync();
        }

        // POST api/accountHistory/credit
        [HttpPost("credit")]
        public async Task<int?> Credit([FromBody] TransactionDto transactionDto)
        {
            if (transactionDto == null)
            {
                throw new WrongRequestException();
            }

            return await _addCreditTransactionCommandHandler.ExecuteAsync(new AddTransactionInputOptions
            {
                Amount = transactionDto.Amount,
                OperationId = (int)Operations.Credit
            });
        }

        // POST api/accountHistory/debit
        [HttpPost("debit")]
        public async Task<int?> Debit([FromBody] TransactionDto transactionDto)
        {
            if (transactionDto == null)
            {
                throw new WrongRequestException();
            }

            return await _addDebitTransactionCommandHandler.ExecuteAsync(new AddTransactionInputOptions
            {
                Amount = transactionDto.Amount,
                OperationId = (int)Operations.Debit
            });
        }
    }
}
