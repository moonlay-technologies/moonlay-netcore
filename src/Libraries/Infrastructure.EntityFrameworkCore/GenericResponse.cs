using System;

namespace Core.Domain
{
    public class GenericResponse
    {
        public GenericResponse(bool success, string message = null, string requestId = null)
        {
            Success = success;
            Message = message;

            _requestId = requestId;
        }

        public bool Success { get; }

        public string Message { get; }

        private string _requestId;

        public string RequestId { get => _requestId ?? Guid.NewGuid().ToString(); set => _requestId = value; }
    }

    public class GenericResponse<T> : GenericResponse
    {
        public GenericResponse(bool success, T data, string message = null, string requestId = null) : base(success, message, requestId)
        {
            Data = data;
        }

        public T Data { get; }
    }
}