using RecipeApp.Core.Enum;
using RecipeApp.Entity.Entities.Base;

namespace RecipeApp.Service
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public ErrorType? ErrorType { get; set; }

        public ServiceResponse(bool success, ErrorType? errorType = null)
        {
            Success = success;
            ErrorType = errorType;
        }
    }

    public class ServiceResponse<T> where T : class
    {
        public bool Success { get; set; }
        public ErrorType? ErrorType { get; set; }
        public T Data { get; set; }

        public ServiceResponse(bool success, ErrorType? errorType = null, T data = null)
        {
            Success = success;
            ErrorType = errorType;
            Data = data;
        }
    }
}