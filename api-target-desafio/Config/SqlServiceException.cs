using System;
using System.Net;

namespace api_target_desafio.Config
{
    public class SqlServiceException : Exception
    {

        public HttpStatusCode Status { get; private set; }

        public SqlServiceException()
        { }

        public SqlServiceException(HttpStatusCode status,string message): base(message)
        {
            Status = status;
        }
      
      
    }
}
