using AccountHistory.Core.Exceptions;

namespace AccountHistory.Common.Domain.Exceptions
{
    public class InvalidTransactionException : BaseException
    {
        public InvalidTransactionException(string message = "Invalid transaction") : base(DomainErrorCodes.InvalidTransaction, message)
        {
        }
    }
}
