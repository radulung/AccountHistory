using System.Threading.Tasks;

namespace AccountHistory.Core
{
    public interface IRetriever
    {
    }

    public interface IRetriever<TOut> : IRetriever
    {
        Task<TOut> GetAsync();
    }

    public interface IRetriever<TIn, TOut> : IRetriever
    {
        Task<TOut> GetAsync(TIn input);
    }
}
