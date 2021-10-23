using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace api_target_desafio.SqlConnector
{
   public abstract class SqlBase
    {
        protected string sConnection = String.Empty;
        protected SqlConnection Connection;
        public virtual SqlConnection Config(string connStr = "")
        {
            if (String.IsNullOrEmpty(connStr))
            {
                return Connection;
            }
            sConnection = connStr;
            Connection = new SqlConnection(sConnection);
            return null;
        }

#pragma warning disable IDE0022 // Usar o corpo do bloco para métodos
#pragma warning disable CS1998
        public virtual async  Task<object> Read(int? id) => throw new System.NotImplementedException();
// Usar o corpo do bloco para métodos

        public virtual async Task<object> ReadRelation(Dictionary<string, string> tables, int? id) => throw new System.NotImplementedException();

        public virtual async Task<bool> Update(object model,int id) => throw new System.NotImplementedException();

        public virtual async Task<bool> Query(string query) => throw new System.NotImplementedException();


        public virtual async Task<object> RangeDateTime(Dictionary<string, string> tables, DateTime start, DateTime end) => throw new System.NotImplementedException();
        public virtual async Task<bool> Insert(object model) => throw new System.NotImplementedException();

        public virtual async Task<int> InsertRelation(object model) => throw new System.NotImplementedException();

#pragma warning restore IDE0022
#pragma warning restore CS1998
    }
}
