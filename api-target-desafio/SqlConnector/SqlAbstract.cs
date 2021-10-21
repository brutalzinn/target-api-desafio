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
            sConnection = connStr;
            Connection = new SqlConnection(sConnection);
        }

        public abstract void CheckValidation();

        public abstract bool Insert(object model);

        public virtual int InsertRelation(object model)
        {
            throw new System.NotImplementedException();
        }

    }
}
