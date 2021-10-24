using System.Collections.Generic;
using System.Text;

namespace api_target_desafio.SqlConnector
{

    //query builder refactor 24/10/2021
    public static class QueryBuilder
    {
        public enum QueryBuilderEnum
        {
            UPDATE = 0,
            SELECT = 1,
            INSERT = 2,
            SELECT_JOIN = 3,
            TABLE_EXISTS = 4
        }
        private static string QueryIfTable(string ModelName, Dictionary<string, string> columns, string select = null, string condition = null)
        { 
           string query = $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{ModelName}') SELECT 1 ELSE SELECT 0";
           return query;
        }
        private static string QueryUpdate(string ModelName, Dictionary<string, string> columns, string select = null, string condition = null)
        {
            StringBuilder SQL_BUILDER = new StringBuilder();
            SQL_BUILDER.Append($"UPDATE {ModelName} SET ");
            foreach (var item in columns)
            {
                SQL_BUILDER.Append($"{item.Key} = '{item.Value}',");
            }
            SQL_BUILDER.Remove(SQL_BUILDER.Length - 1, 1);
            if (condition != null)
            {
                SQL_BUILDER.Append($" {condition}");
            }
            return SQL_BUILDER.ToString();
        }

        private static string QueryInsert(string ModelName, Dictionary<string, string> columns, string select = null, string condition = null)
        {
            StringBuilder SQL_BUILDER = new StringBuilder();

            SQL_BUILDER.Append($"INSERT INTO {ModelName} (");
            foreach (var item in columns)
            {
                SQL_BUILDER.Append($"{item.Key},");
            }
            SQL_BUILDER.Remove(SQL_BUILDER.Length - 1, 1);
            SQL_BUILDER.Append(") VALUES (");
            foreach (var item in columns)
            {
                SQL_BUILDER.Append($"{item.Value},");
            }
            SQL_BUILDER.Append(")");
            return SQL_BUILDER.ToString();
        }

        private static string QuerySelect(string ModelName, Dictionary<string, string> columns, string select = null, string condition = null)
        {
            StringBuilder SQL_BUILDER = new StringBuilder();
            SQL_BUILDER.Append($"SELECT ");
            foreach (var item in columns)
            {
                SQL_BUILDER.Append($"{item.Key},");
            }
            SQL_BUILDER.Remove(SQL_BUILDER.Length - 1, 1);
            SQL_BUILDER.Append($" FROM {ModelName}");
            if (condition != null)
            {
                SQL_BUILDER.Append($" {condition}");
            }           
            return SQL_BUILDER.ToString();
        }
        private static string SpliterSeparator(string ModelId, string words)
        {
            StringBuilder TABLE_COLUMNS = new StringBuilder();
            words = words.Replace(" ", "");
            for (var i = 0; i < words.Split(',').Length; i++)
            {
                TABLE_COLUMNS.Append($"{ModelId}.{words.Split(',')[i]} AS {ModelId}{words.Split(',')[i]},");

            }
            return TABLE_COLUMNS.ToString();
        }
        private static string QuerySelectJoin(string ModelName, Dictionary<string, string> columns, string select = null, string condition = null)
        {
            StringBuilder SQL_BUILDER = new StringBuilder();
            StringBuilder _SQL_NAMES = new StringBuilder();
            string uniqueModel = ModelName.ToLower().Substring(0, 5);
            var modelColumns = SpliterSeparator(uniqueModel,select);
            _SQL_NAMES.Append($"SELECT {modelColumns}");
            foreach (var item in columns)
            {
                string name = item.Key.ToLower().Substring(0, 5);
                SQL_BUILDER.Append($" INNER JOIN {item.Key} AS {name} ON {name}.Id = {uniqueModel}.{item.Key}_Id");          
                _SQL_NAMES.Append($"{name}.Id AS {name}Id, {SpliterSeparator(name,item.Value)}");
                
            }
         
            _SQL_NAMES.Remove(_SQL_NAMES.Length - 1, 1);
            string query = $"{_SQL_NAMES} FROM {ModelName} AS {uniqueModel} {SQL_BUILDER}";
            SQL_BUILDER.Clear();
            SQL_BUILDER.Append(query);
            if (condition != null)
            {
                SQL_BUILDER.Append($" {condition}");
            }
            return SQL_BUILDER.ToString();
        }

        public static string Query(QueryBuilderEnum type, string ModelName, Dictionary<string, string> columns = null, string select = null, string condition = null)
        {
            switch (type)
            {
                // QUERY UPDATE
                case QueryBuilderEnum.UPDATE:
                    return QueryUpdate(ModelName, columns, select, condition);
                case QueryBuilderEnum.INSERT:
                    return QueryInsert(ModelName, columns, select, condition);
                case QueryBuilderEnum.SELECT:
                    return QuerySelect(ModelName, columns, select, condition);
                case QueryBuilderEnum.SELECT_JOIN:
                    return QuerySelectJoin(ModelName, columns, select, condition);
                case QueryBuilderEnum.TABLE_EXISTS:
                    return QueryIfTable(ModelName, columns, select, condition);
            }
            return "";
        }

        public static string QueryConcat(params string[] query)
        {
            string result = null;
            for (int i = 0; i < query.Length; i++)
            {
                result += $"{query[i]};";
            }
            return result;
        }
    }
}
