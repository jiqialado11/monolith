using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SubContractors.Common
{
    public class SwaggerResultPost
    {
        public bool IsSuccess => Type is ResultType.Ok or ResultType.Accepted or ResultType.Created;
        public int StatusCode => (int) Type;
        public string Message { get; set; }

        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultType Type { get; set; }
    }

    public class SwaggerResultPost<T>
    {
        public bool IsSuccess => Type is ResultType.Ok or ResultType.Accepted or ResultType.Created;
        public int StatusCode => (int)Type;
        public string Message { get; set; }
        public T Data { get; set; }

        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultType Type { get; set; }
    }

    public class SwaggerResultGet<T>
    {
        public bool IsSuccess => Type is ResultType.Ok or ResultType.Accepted or ResultType.Created;
        public int StatusCode => (int) Type;
        public string Message { get; set; }
        public T Data { get; set; }

        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultType Type { get; set; }
    }

    public class SwaggerResultValidationFailure
    {
        public bool IsSuccess => Type is ResultType.Ok or ResultType.Accepted or ResultType.Created;
        public int StatusCode => (int) Type;
        public string Message { get; set; }
        public IReadOnlyDictionary<string, string> Errors { get; set; }

        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultType Type { get; set; }
    }

    public class SwaggerResultException
    {
        public bool IsSuccess => Type is ResultType.Ok or ResultType.Accepted or ResultType.Created;
        public int StatusCode => (int) Type;
        public string Message { get; set; }

        [JsonIgnore]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultType Type { get; }
    }

    public class SwaggerEmptyJsonSample
    { }
}