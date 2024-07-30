using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WA.Domain.Base
{
    public class ResultObject : ObjectResult
    {
        public ResultObject(HttpStatusCode statusCode, object value) : base(value)
        {
            StatusCode = (int)statusCode;
        }
    }
}
