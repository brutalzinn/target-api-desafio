using System;

namespace api_target_desafio.Config
{
    public class SqlServiceException : Exception
    {
        public SqlServiceException()
        { }

        public SqlServiceException(string message)
            : base(message)
        { }

        public SqlServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
