using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.ApiResultModel
{
    public class ApiResult<T>
    {
        public T? OperationResult { get; set; }

        public ICollection<string> Errors { get; set; }

        public ApiResult(T? operationResult,
            ICollection<string> errors)
        {
            OperationResult = operationResult;
            Errors = errors;
        }

        public static ApiResult<T> Failed(T? operationResult, ICollection<string> errors)
            => new(operationResult, errors);

        public static ApiResult<T> SuccessOk(T? operationResult)
            => new(operationResult, new List<string>() { "Success"});

    }
}
