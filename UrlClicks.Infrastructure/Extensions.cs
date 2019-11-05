using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Pathoschild.Http.Client
{
    public static class Extensions
    {
        public static IClient ToHttpClient(this string url)
        {
            return new FluentClient(url);
        }
        public static IClient ToHttpClient(this Uri uri)
        {
            return new FluentClient(uri);
        }
        public static IClient Fluent(this HttpClient httpClient)
        {
            return new FluentClient(httpClient.BaseAddress,httpClient);
        }        
    }
}

namespace UrlClicks.Infrastructure
{
    public static class Extensions
    {
        public static List<T> ConvertTableEntity<T>(this List<DynamicTableEntity> entities)
        {
            var result = new List<T>();

            Type t1 = typeof(T);
            foreach (var a in entities)
            {
                var dictionary = (IDictionary<string, EntityProperty>)a.Properties;
                dictionary.Add("PartitionKey", new EntityProperty(a.PartitionKey));
                dictionary.Add("RowKey", new EntityProperty(a.RowKey));
                dictionary.Add("Timestamp", new EntityProperty(a.Timestamp));
                var innerresult = (T)Activator.CreateInstance(t1);
                foreach (var property in t1.GetProperties())
                {
                    var value = dictionary[property.Name];
                    if (value != null)
                    {
                        var setter = property.SetMethod;
                        if (setter != null)
                            property.SetValue(innerresult, GetValue(value, property.PropertyType));
                    }
                }
                result.Add(innerresult);
            }
            return result;
        }

        private static object GetValue(EntityProperty source, Type type)
        {
            switch (source.PropertyType)
            {
                case EdmType.Binary:
                    return (object)source.BinaryValue;
                case EdmType.Boolean:
                    return (object)source.BooleanValue;
                case EdmType.DateTime:
                    return (type == typeof(DateTime)) ? (object)source.DateTime : (object)source.DateTimeOffsetValue;
                case EdmType.Double:
                    return (object)source.DoubleValue;
                case EdmType.Guid:
                    return (object)source.GuidValue;
                case EdmType.Int32:
                    return (object)source.Int32Value;
                case EdmType.Int64:
                    return (object)source.Int64Value;
                case EdmType.String:
                    return (object)source.StringValue;
                default: throw new TypeLoadException(string.Format("not supported edmType:{0}", source.PropertyType));
            }
        }
    }
}
