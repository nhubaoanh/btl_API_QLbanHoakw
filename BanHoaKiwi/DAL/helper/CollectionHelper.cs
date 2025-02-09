using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.helper
{
    public static class MessageConvert
    {
        private static readonly JsonSerializerSettings Settings;
        static MessageConvert()
        {
            Settings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static string SerializeObject(this object obj)
        {
            if (obj == null)
                return "";
            return JsonConvert.SerializeObject(obj, Settings);
        }
        public static T DeserializeObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }

        public static Object DeserializeObject(this string json, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(json, type, Settings);
            }
            catch
            {
                return null;
            }
        }
    }
    public static class CollectionHelper
    {
        public static DataTable ConvertTo<T>(this IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entitytype = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entitytype);
            foreach(T item in list)
            {
                DataRow row = table.NewRow();
                foreach(PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;
            if(rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }

        public static IList<T> ConvertTo<T>(this DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            var rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }
        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (prop == null) continue; // Nếu không tìm thấy thuộc tính, bỏ qua cột này

                    Type type = prop.PropertyType;
                    try
                    {
                        object value = row[column.ColumnName];
                        if (value != DBNull.Value)
                        {
                            // Kiểm tra nếu cột chứa JSON
                            if (column.ColumnName.Contains("json", StringComparison.OrdinalIgnoreCase))
                            {
                                var deserializedValue = MessageConvert.DeserializeObject(value.ToString().Replace("$", ""), type);
                                prop.SetValue(obj, deserializedValue, null);
                            }
                            else if (type == typeof(string))
                            {
                                prop.SetValue(obj, Convert.ToString(value), null);
                            }
                            else if (type == typeof(float) || type == typeof(Single))
                            {
                                prop.SetValue(obj, Convert.ToSingle(value), null);
                            }
                            else if (type == typeof(DateTime) || (Nullable.GetUnderlyingType(type) == typeof(DateTime)))
                            {
                                var t = Nullable.GetUnderlyingType(type) ?? type;
                                var safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                                prop.SetValue(obj, safeValue, null);
                            }
                            else if (Nullable.GetUnderlyingType(type) != null) // Xử lý Nullable
                            {
                                var t = Nullable.GetUnderlyingType(type) ?? type;
                                var safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                                prop.SetValue(obj, safeValue, null);
                            }
                            else
                            {
                                prop.SetValue(obj, Convert.ChangeType(value, type), null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error setting property '{column.ColumnName}' to object '{typeof(T).Name}': {ex.Message}");
                        throw; // Nếu cần thiết, bạn có thể không throw lỗi
                    }
                }
            }
            return obj;
        }

        //public static T CreateItem<T>(DataRow row)
        //{
        //    T obj = default(T);
        //    if (row != null)
        //    {
        //        obj = Activator.CreateInstance<T>();
        //        foreach (DataColumn column in row.Table.Columns)
        //        {
        //            PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
        //            if (prop == null) continue;
        //            Type type = prop.PropertyType;
        //            try
        //            {
        //                object value = row[column.ColumnName];
        //                if (value != DBNull.Value)
        //                {
        //                    if (column.ColumnName.Contains("json"))
        //                    {
        //                        prop.SetValue(obj, MessageConvert.DeserializeObject(("" + value).Replace("$", ""), type), null);
        //                    }
        //                    else if (type.Name == "String")
        //                    {
        //                        prop.SetValue(obj, Convert.ToString(value), null);
        //                    }
        //                    else if (type.Name == "Single")
        //                    {
        //                        prop.SetValue(obj, Convert.ToSingle(value), null);
        //                    }
        //                    else if (type.Name == "Nullable`1" || type.Name == "DateTime")
        //                    {
        //                        var t = Nullable.GetUnderlyingType(type) ?? type;
        //                        var safeValue = (value == null) ? null : Convert.ChangeType(value, t);
        //                        prop.SetValue(obj, safeValue, null);
        //                    }
        //                    else
        //                    {
        //                        prop.SetValue(obj, value, null);
        //                    }
        //                }
        //            }
        //            catch
        //            {
        //                // You can log something here
        //                throw;
        //            }
        //        }
        //    }

        //    return obj;
        //}

        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            var table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }
    }
}
