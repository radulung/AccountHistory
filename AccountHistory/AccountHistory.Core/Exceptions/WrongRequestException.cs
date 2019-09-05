namespace AccountHistory.Core.Exceptions
{
    public class WrongRequestException : BaseException
    {
        public WrongRequestException(string message = "Wrong api request") : base(StandardApiErrorCodes.WrongRequest, message)
        {
        }
    }
}
