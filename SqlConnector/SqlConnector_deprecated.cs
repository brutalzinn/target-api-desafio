//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Text.RegularExpressions;

//namespace api_target_desafio.Utils
//{
//    public class SqlConnector
//    {

//        private string ModelName { get; set; } = String.Empty;

//        private string connStr = String.Empty;

//        private BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

//        private List<string> ModelObject { get; set; } = new List<string>();


//        public SqlConnector(object classReference, string connection)
//        {
//            connStr = connection;
//            ModelName = classReference.GetType().Name;

//            var fieldValues = classReference.GetType()
//                                 .GetFields(bindingFlags).ToList();
//            foreach (var fields in fieldValues)
//            {

//                MatchCollection matches = Regex.Matches(fields.Name, @"<(.*)>");
//                foreach (Match match in matches)
//                {
//                    ModelObject.Add(match.Groups[1].ToString());
//                    Debug.WriteLine($"ADDED {match.Groups[1]} to List");
//                }

//            }
//        }
//        private bool getMatchFields(string field, string value)
//        {
//            Debug.WriteLine($"COMPARE {field} {value}");
//            return Regex.Matches(field, @"<(.*)>").ToList().Any(e => e.Groups[1].ToString().Contains(value));

//        }
//        private List<SqlParameter> ParamGenerator(object classReference)
//        {
//            List<SqlParameter> _list = new List<SqlParameter>();
//            var fieldValue = classReference.GetType().GetFields(bindingFlags).ToList();
//            foreach (string Field in ModelObject)
//            {

//                Debug.WriteLine($"FIELD:{Field}");
//                var item = fieldValue.FirstOrDefault(f => getMatchFields(f.Name, Field));
//                if (item != null && Field != "Id")
//                    //Debug.WriteLine($"{Field.ToUpper()}, {fieldValue}");
//                    _list.Add(new SqlParameter($"@{Field.ToUpper()}", item.GetValue(classReference)));


//            }
//            return _list;
//        }

//        // TODO: WE NEED TO REFACTOR THIS FUNCTION AND EXPAND IT TO ALL OTHERS METHODS

//        public void Insert(object classReference)
//        {
//            string commandText = "INSERT INTO PessoaModel (NomeCompleto,CPF,DataNascimento) VALUES (@NOMECOMPLETO,@CPF,@DATANASCIMENTO)";
//            var connection = new SqlConnection(connStr);

//            SqlCommand command = new SqlCommand(commandText, connection);

//            foreach (var item in ParamGenerator(classReference))
//            {
//                command.Parameters.Add(item);

//            }
//            connection.Open();
//            command.ExecuteNonQuery();
//            connection.Close();

//        }
//    }
//}
