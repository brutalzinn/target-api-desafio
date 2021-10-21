using System;
using System.Data.SqlClient;

namespace api_target_desafio.SqlConnector
{
   public abstract class SqlAbstract
    {
        protected string sConnection = String.Empty;
        protected SqlConnection Connection;
        public virtual void Config(string connStr)
        {
            Connection = new SqlConnection(connStr);
        }

        public abstract void CheckValidation();

        public abstract bool Insert(object model);

    }
}
