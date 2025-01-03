﻿using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public bool Success{ get; }

        public string Message { get; }

        public HttpStatusCode StatusCode { get; }
        public Result(string message,bool success,HttpStatusCode statusCode):this(success,statusCode)
        {
            Message = message;
        }
        public Result(bool success,HttpStatusCode statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }
    }
}
