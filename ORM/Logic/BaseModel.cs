using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ORM
{
    public abstract class BaseModel
    {
        public List<SqlParameter> ToSqlParameters()
        {
            return ToSqlParameters(null);
        }
        public List<SqlParameter> ToSqlParameters(string[] parameterNames)
        {
            var list = new List<SqlParameter>();

            Type type = GetType();

            //for each property of object of Item
            foreach (PropertyInfo propInfo in type.GetProperties())
            {

                //for each custom attribute on the property loop
                foreach (FieldAttribute attr in propInfo.GetCustomAttributes(typeof(FieldAttribute), false))
                {
                    if (attr != null)
                    {
                        if (parameterNames == null || parameterNames.Contains(attr.FieldName))
                        {
                            if (propInfo.PropertyType == typeof(DateTime) && ((DateTime)propInfo.GetValue(this, null)) == DateTime.MinValue)
                            {
                                var param = new SqlParameter("@" + attr.FieldName, DBNull.Value) { DbType = DbType.DateTime };
                                list.Add(param);
                            }
                            else
                            {
                                list.Add(new SqlParameter("@" + attr.FieldName, propInfo.GetValue(this, null)));
                            }
                        }
                    }
                }
            }

            return list;
        }

        public void FromReader(SqlDataReader reader)
        {
            Type type = GetType();

            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                foreach (FieldAttribute attr in propInfo.GetCustomAttributes(typeof(FieldAttribute), false))
                {
                    if (attr != null)
                    {
                        object obj = null;

                        if (propInfo.PropertyType == typeof(int))
                        {
                            obj = reader[attr.FieldName] == DBNull.Value ? 0 : Convert.ToInt32(reader[attr.FieldName]);
                        }
                        if (propInfo.PropertyType == typeof(int?))
                        {
                            if (reader[attr.FieldName] != DBNull.Value)
                            {
                                obj = Convert.ToInt32(reader[attr.FieldName]);
                            }
                        }
                        else if (propInfo.PropertyType == typeof(long))
                        {
                            obj = Convert.ToInt64(reader[attr.FieldName]);
                        }
                        else if (propInfo.PropertyType == typeof(decimal))
                        {
                            obj = Convert.ToDecimal(reader[attr.FieldName]);
                        }
                        else if (propInfo.PropertyType == typeof(decimal?))
                        {
                            if (reader[attr.FieldName] != DBNull.Value)
                            {
                                obj = Convert.ToDecimal(reader[attr.FieldName]);
                            }
                        }
                        else if (propInfo.PropertyType == typeof(string))
                        {
                            if (reader[attr.FieldName] != DBNull.Value)
                            {
                                obj = Convert.ToString(reader[attr.FieldName]);
                            }
                        }
                        else if (propInfo.PropertyType == typeof(DateTime))
                        {
                            obj = reader[attr.FieldName] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader[attr.FieldName]);
                        }
                        else if (propInfo.PropertyType == typeof(DateTime?))
                        {
                            if (reader[attr.FieldName] != DBNull.Value)
                            {
                                obj = Convert.ToDateTime(reader[attr.FieldName]);
                            }
                        }
                        else if (propInfo.PropertyType == typeof(bool))
                        {
                            obj = Convert.ToBoolean(reader[attr.FieldName]);
                        }
                        else if (propInfo.PropertyType == typeof(bool?))
                        {
                            if (reader[attr.FieldName] != DBNull.Value)
                            {
                                obj = Convert.ToBoolean(reader[attr.FieldName]);
                            }
                        }

                        // Finally, set the value
                        propInfo.SetValue(this, obj, null);
                    }
                }
            }
        }
    }
}
