using AccountHistory.Api.SignalR;
using AccountHistory.Common.Domain.Dtos;
using AccountHistory.Common.Domain.Enums;
using AccountHistory.Core.Exceptions;
using AccountHistory.Domain.Commands.Handler;
using AccountHistory.Domain.Commands.InputOptions;
using AccountHistory.Domain.Retriever.Retrievers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public AccountHistoryController(AddCreditTransactionCommandHandler addCreditTransactionCommandHandler,
            AddDebitTransactionCommandHandler addDebitTransactionCommandHandler,
            TransactionsRetriever transactionsRetriever,
            CurrentBalanceRetriever currentBalanceRetriever,
            IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _addCreditTransactionCommandHandler = addCreditTransactionCommandHandler;
            _addDebitTransactionCommandHandler = addDebitTransactionCommandHandler;
            _transactionsRetriever = transactionsRetriever;
            _currentBalanceRetriever = currentBalanceRetriever;
            _hubContext = hubContext;
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

            var currentBalance = await _addCreditTransactionCommandHandler.ExecuteAsync(new AddTransactionInputOptions
            {
                Amount = transactionDto.Amount,
                OperationId = (int)Operations.Credit
            });

            if (currentBalance < 10)
            {
                await _hubContext.Clients.All.BroadcastMessage("Your balance has dropped below 10!");
            }

            return currentBalance;
        }

        // POST api/accountHistory/debit
        [HttpPost("debit")]
        public async Task<int?> Debit([FromBody] TransactionDto transactionDto)
        {
            if (transactionDto == null)
            {
                throw new WrongRequestException();
            }

            var currentBalance = await _addDebitTransactionCommandHandler.ExecuteAsync(new AddTransactionInputOptions
            {
                Amount = transactionDto.Amount,
                OperationId = (int)Operations.Debit
            });

            await _hubContext.Clients.All.BroadcastMessage($"Your account has been filled with ${transactionDto.Amount} money!");

            return currentBalance;
        }


    }
}
