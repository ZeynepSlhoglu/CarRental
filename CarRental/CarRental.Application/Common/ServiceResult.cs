namespace CarRental.CarRental.Application.Common
{
    public class ServiceResult
    {
        public bool SuccessStatus { get; private set; }
        public string Message { get; private set; }

        private ServiceResult(bool success, string message)
        {
            SuccessStatus = success;
            Message = message;
        }

        public static ServiceResult Success(string message) => new ServiceResult(true, message);
        public static ServiceResult Failure(string message) => new ServiceResult(false, message);
    }
}
