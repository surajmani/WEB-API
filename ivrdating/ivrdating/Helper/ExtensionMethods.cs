using ivrdating.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ivrdating.Helper
{
    public static class ExtensionMethods
    {
        public static Dictionary<string, object> ReturnAllParamerterValues(IList<PropertyInfo> properties, object type, string function)
        {
            Dictionary<string, object> rnt = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                rnt.Add(property.Name, property.GetValue(type, null));
            }
            rnt.Add("Function", function);
            return rnt;
        }
        public static object CreateItemFromRow(DataRow row, IList<PropertyInfo> properties, object type)
        {
            //IList<PropertyInfo> properties = typeof(MemberDetailVM).GetProperties().ToList();

            string val = "";
            foreach (var property in properties)
            {
                val = row[property.Name] == null ? "" : row[property.Name].ToString();
                property.SetValue(type, val, null);
            }
            return type;
        }

        public static string SerializeToPlainText(IList<PropertyInfo> properties, object type)
        {
            List<string> val = new List<string>();
            foreach (var property in properties)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(property.GetValue(type, null))))
                    val.Add(Convert.ToString(property.GetValue(type, null)));
            }
            //string abc = string.Join("|", val);
            return string.Join("|", val);
        }
    }
}