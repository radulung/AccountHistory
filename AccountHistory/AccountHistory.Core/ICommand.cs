using System.Threading.Tasks;

namespace AccountHistory.Core
{
    public interface ICommand
    {

    }

    public interface ICommand<TIn, TOut> : ICommand where TIn : ICommandInput
    {
        Task<TOut> ExecuteAsync(TIn inputOptions);
    }
}
