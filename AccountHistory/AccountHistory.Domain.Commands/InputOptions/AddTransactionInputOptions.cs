using AccountHistory.Core;

namespace AccountHistory.Domain.Commands.InputOptions
{
    public class AddTransactionInputOptions : ICommandInput
    {
        public double Amount { get; set; }
        public int OperationId { get; set; }
    }
}
