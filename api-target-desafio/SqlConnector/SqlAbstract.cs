using System;
using System.Collections.Generic;
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

        public abstract object Read(int? id);

        public abstract object ReadRelation(Dictionary<string, string> tables, int? id);

        public abstract bool Insert(object model);

        public virtual int InsertRelation(object model)
        {
            throw new System.NotImplementedException();
        }

    }
}
