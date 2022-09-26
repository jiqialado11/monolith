using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace SubContractors.Common
{
    public class Result<T>
    {
        public bool IsSuccess => Type is ResultType.Ok or ResultType.Accepted or ResultType.Created;
        public int StatusCode => (int) Type;
        public string Message { get; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IReadOnlyDictionary<string, List<string>> Errors { get; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Data { get; }
        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultType Type { get; }        
        [JsonConstructor]
        public Result(ResultType type, string message, T data, IReadOnlyDictionary<string, List<string>> errors)
        {
            Type = type;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public Result(ResultType type, string message)
        {
            Type = type;
            Message = message;
        }
    }

    public static class Result
    {
        #region NonGeneric

        public static Result<Unit> Ok(string message = "Operation was successful")
        {
            return new Result<Unit>(ResultType.Ok, message, Unit.Value, null);
        }

        public static Result<Unit> Accepted(string message = "Operation was successful")
        {
            return new Result<Unit>(ResultType.Accepted, message, Unit.Value, null);
        }

        public static Result<Unit> Success(ResultType type, string message = "Operation was successful")
        {
            return new Result<Unit>(type, message, Unit.Value, null);
        }

        public static Result<Unit> Fail(ResultType type, string message = "Operation Failed")
        {
            return new Result<Unit>(type, message, Unit.Value, null);
        }

        public static Result<Unit> NotFound(string message = "Not found")
        {
            return new Result<Unit>(ResultType.NotFound, message, Unit.Value, null);
        }

        public static Result<Unit> Error(Exception exception)
        {
            return new Result<Unit>(ResultType.InternalServerError, exception.Message, Unit.Value, null);
        }

        public static Result<Unit> Error(ResultType type, string message = "Operation Failed", IReadOnlyDictionary<string, List<string>> errors = default)
        {
            return new Result<Unit>(type, message, Unit.Value, errors);
        }

        public static Result<Unit> Unauthorized(string message = "Unauthorized access")
        {
            return new Result<Unit>(ResultType.Unauthorized, message, Unit.Value, null);
        }

        #endregion

        #region Generic

        public static Result<T> Ok<T>(string message = "Operation was successful", T value = default)
        {
            return new Result<T>(ResultType.Ok, message, value, null);
        }

        public static Result<T> Success<T>(ResultType type, string message = "Operation was successful", T data = default)
        {
            return new Result<T>(type, message, data, null);
        }

        public static Result<T> Fail<T>(ResultType type, string message = "Operation failed")
        {
            return new Result<T>(type, message, default, null);
        }

        public static Result<T> NotFound<T>(string message = "Not found")
        {
            return new Result<T>(ResultType.NotFound, message, default, null);
        }
        public static Result<T> Error<T>(Exception exception)
        {
            return new Result<T>(ResultType.InternalServerError, exception.Message, default, null);
        }
        public static Result<T> Error<T>(ResultType type, string message = "Operation failed", IReadOnlyDictionary<string, List<string>> errors = default)
        {
            return new Result<T>(type, message, default, errors);
        }


        #endregion

        #region Convert

        public static Result<Unit> ToResult<T>(this Result<T> result)
        {
            return new Result<Unit>(result.Type, result.Message, Unit.Value, result.Errors);
        }

        public static Result<T> FromResult<T>(this Result<Unit> result)
        {
            return new Result<T>(result.Type, result.Message, default, result.Errors);
        }

        public static Result<TToResponseType> Convert<TToResponseType, TFromResultType>(this Result<TFromResultType> result)
        {
            return new Result<TToResponseType>(result.Type, result.Message, default, result.Errors);
        }

        #endregion
    }

    public enum ResultType
    {
        Ok = HttpStatusCode.OK,
        Accepted = HttpStatusCode.Accepted,
        Created = HttpStatusCode.Created,
        NoContent = HttpStatusCode.NoContent,
        BadRequest = HttpStatusCode.BadRequest,
        Unauthorized = HttpStatusCode.Unauthorized,
        Forbidden = HttpStatusCode.Forbidden,
        NotFound = HttpStatusCode.NotFound,
        UnsupportedMediaType = HttpStatusCode.UnsupportedMediaType,
        InternalServerError = HttpStatusCode.InternalServerError,
        BadGateway = HttpStatusCode.BadGateway,
        ServiceUnavailable = HttpStatusCode.ServiceUnavailable
    }
}