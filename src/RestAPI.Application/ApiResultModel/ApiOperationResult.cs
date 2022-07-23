using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Application.ApiResultModel
{
    public class ApiOperationResult
    {
        ICollection<string> Errors { get; set; }
        bool Success { get; set; }

    }
}
