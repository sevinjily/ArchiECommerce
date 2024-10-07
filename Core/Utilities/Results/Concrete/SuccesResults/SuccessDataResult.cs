using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete.SuccesResults
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message,HttpStatusCode statusCode):base(data,message,true,statusCode)
        {
            
        }
        public SuccessDataResult(T data, HttpStatusCode statusCode) : base(data, true, statusCode)
        {
            
        }
        public SuccessDataResult( string message, HttpStatusCode statusCode) : base(default,message, true, statusCode)
        {
            
        }
        public SuccessDataResult(HttpStatusCode statusCode) : base(default, true, statusCode)
        {

        }
    }
}
