using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ORM.Results;

namespace ORM
{
    public class Database
    {
        #region Stored Procedure

        public static ProcedureResult StoredProcedure(string procedureName)
        {
            return StoredProcedure(procedureName, new List<SqlParameter>());
        }

        public static ProcedureResult StoredProcedure(string procedureName, SqlParameter parameter)
        {
            return StoredProcedure(procedureName, new List<SqlParameter> {parameter});
        }
 
        /// <summary>
        /// Executes stored procedure
        /// </summary>        
        public static ProcedureResult StoredProcedure(string procedureName, IEnumerable<SqlParameter> parameters)
        {
            return StoredProcedure(procedureName, parameters, null);
        }

        public static ProcedureResult StoredProcedure(string procedureName, Dictionary<string, dynamic> values, string outputParameterName = null)
        {
            var parameters = values.Select(i => new SqlParameter(i.Key, i.Value)).ToList();

            if (!string.IsNullOrWhiteSpace(outputParameterName))
            {
                parameters.Add(new SqlParameter(outputParameterName, 0));
                return StoredProcedure(procedureName, parameters, outputParameterName);                
            }

            return StoredProcedure(procedureName, parameters);
        }
       
        /// <summary>
        /// Executes stored procedure
        /// </summary>
        public static ProcedureResult StoredProcedure(string procedureName, IEnumerable<SqlParameter> parameters, string outputParameterName)
        {
            ProcedureResult result;

            using (var conn = new SqlConnection(Configuration.ConnectionString))
            {
                var command = conn.CreateCommand();
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();

                if (!string.IsNullOrEmpty(outputParameterName))
                {
                    var param = parameters.First(i => i.ParameterName.ToLower() == outputParameterName.ToLower());
                    param.Direction = ParameterDirection.Output;
                }

                foreach (SqlParameter parameter in parameters)
                {
                    
                    //    if (value.Value is string)
                    //    {
                    //        parameters.Add(new SqlParameter(value.Key, value.Value));
                    //    }
                    //    else if (value.Value is DataTable)
                    //    {
                    //        parameters.AddW(new SqlParameter(value.Key, value.Value));

                    //    }
                    command.Parameters.Add(parameter);
                }

                using (command)
                {
                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();

                        result = string.IsNullOrEmpty(outputParameterName)
                                     ? new ProcedureResult { Status = Status.Success }
                                     : new ProcedureIdentityResult
                                         {
                                             Status = Status.Success, Identity = Convert.ToInt32(command.Parameters[outputParameterName].Value)
                                         };

                        conn.Close();
                    }
                    catch (SqlException exception)
                    {
                        result = string.IsNullOrEmpty(outputParameterName)
                                     ? new ProcedureResult()
                                     : new ProcedureIdentityResult();

                        result.Status = Status.Error;
                        result.SetError(exception.Message);
                    }
                }
            }

            return result;
        }

        #endregion

        #region Straighforward Query

        /// <summary>
        /// Straightforwardly executes SQL query
        /// </summary>
        /// /// <returns>-1 if there was an error or 0 if successfull. Altrenatively can return a newly created Id.</returns>
        public static int ExecuteQuery(string query)
        {
            return ExecuteQuery(query, null);
        }

        public static int ExecuteQuery(string query, string returnFieldName)
        {
            int id = 0;

            try
            {
                using (var conn = new SqlConnection(Configuration.ConnectionString))
                {
                    var command = conn.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    using (command)
                    {
                        conn.Open();

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (!string.IsNullOrEmpty(returnFieldName) && ColumnExists(dr, returnFieldName))
                                {
                                    id = Convert.ToInt32(dr[returnFieldName]);
                                }
                                else if(returnFieldName == null && dr.FieldCount > 0)
                                {
                                    id = Convert.ToInt32(dr[0]);
                                }
                            }
                        }

                        conn.Close();
                    }
                }
            }
            catch (SqlException)
            {
                return -1;
            }

            return id;
        }

        private static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Properties

        private static SqlParameter OutputParameter(string parameterName)
        {
            return new SqlParameter {ParameterName = parameterName, Direction = ParameterDirection.Output};
        }

        #endregion
    }
}
