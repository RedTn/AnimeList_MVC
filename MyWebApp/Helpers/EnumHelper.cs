using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;

namespace MyWebApp.Helpers
{
    public static class EnumHelper
    {
        public static List<String> GetEnumNames(Type enumType)
        {
            return enumType.GetMembers(BindingFlags.Public | BindingFlags.Static).Select(m =>
            {
                var attr = m.GetCustomAttribute<EnumMemberAttribute>();
                if (attr != null)
                    return attr.Value;
                return m.Name;
            }).ToList();
        }
    }
}