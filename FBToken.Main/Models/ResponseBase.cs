namespace FBToken.Main.Models
{
    public abstract class ResponseBase
    {
        public string Message { get; protected set; }
        public bool Success { get; protected set; }

        public ResponseBase(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}