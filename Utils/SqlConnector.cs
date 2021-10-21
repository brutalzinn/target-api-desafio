using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace api_target_desafio.Utils
{
    public class SqlConnector
    {
        public static void Query(string query)
        {

        }
        private string ModelName { get; set; } = String.Empty;

        private string connStr = String.Empty;

        private List<string> ModelObject { get; set; } = new List<string>();
        public SqlConnector(object classReference, string connection)
        {
            connStr = connection;
            string modelName = classReference.GetType().Name;
            var bindingFlags = BindingFlags.Instance |
                         BindingFlags.NonPublic |
                         BindingFlags.Public;
            var fieldValues = classReference.GetType()
                                 .GetFields(bindingFlags).ToList();
            foreach (var fields in fieldValues)
            {
                Debug.WriteLine(fields.GetValue(classReference));
                MatchCollection matches = Regex.Matches(fields.Name, @"<(.*)>");
               foreach(Match match in matches)
                {
                    ModelObject.Add(match.Groups[1].ToString());
                    Debug.WriteLine($"ADDED {match.Groups[1]} to List");
                }

            }
        }
        private List<SqlParameter> ParamGenerator(dynamic instance)
        {
            List<SqlParameter> _list = null;
            foreach (string Field in ModelObject)
            {
                if (Field != "Id")
                {
                    Debug.WriteLine($"{Field.ToUpper()}, {instance}");
                  //  _list.Add(new SqlParameter($"@{Field.ToUpper()}", instance[Field]));
                    
                }
            }
            return _list;
        }
        public void Insert(dynamic classReference)
        {
            string commandText = "INSERT INTO PessoaModel (NomeCompleto,CPF,DataNascimento) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO)";
            var connection = new SqlConnection(connStr);

            SqlCommand command = new SqlCommand(commandText, connection);

            foreach(var item in ParamGenerator(classReference))
            {
                command.Parameters.Add(item);

            }
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
}
