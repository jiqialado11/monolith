using System;
using System.ComponentModel;
using System.Reflection;

namespace SubContractors.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            
            if (fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return value.ToString();
        }
    }
}