using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using ORM.Results;

namespace ORM
{
    public class DataBase<T> where T : BaseModel, new()
    {
        public static SelectResult<T> GetModel<T>(string procedureName)
        {
            return GetModel<T>(procedureName, new SqlParameter[] { });
        }
        public static SelectResult<T> GetModel<T>(string procedureName, SqlParameter[] parameters)
        {
            var messages = new List<T>();

            using (var conn = new SqlConnection(Configuration.ConnectionString))
            {
                //int timeout = int.Parse(ConfigurationManager.AppSettings["DatabaseTimeout"].Trim());
                var cmd = new SqlCommand
                {
                    CommandText = procedureName,
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    //CommandTimeout = timeout
                };

                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var t = (T)Activator.CreateInstance(typeof(T));

                        MethodInfo method = typeof(T).GetMethod("FromReader");
                        method.Invoke(t, new object[] { dr });
                        messages.Add(t);
                    }
                }
            }

            return new SelectResult<T> { Values = messages.OfType<T>() };
        }
    }
}
