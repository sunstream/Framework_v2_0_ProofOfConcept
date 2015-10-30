using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProofOfConcept.Utils
{
    public static class ExtensionMethods
    {
        public static Type GetUnderlyingType(this MemberInfo member)
        {
            try
            {
                Type pageMemberType;
                switch (member.MemberType)
                {
                    case MemberTypes.Field: pageMemberType = ((FieldInfo)member).FieldType; break;
                    case MemberTypes.Property: pageMemberType = ((PropertyInfo)member).PropertyType; break;
                    default: pageMemberType = typeof(Object); break;
                }
                return pageMemberType;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(
                    string.Format("Failed to extract type information for page member {0}", member.Name), e);
            }
        }

        public static void ShallowConvert<T, U>(this T parent, U child)
        {
            foreach (PropertyInfo property in parent.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(parent, null), null);
                }
            }
        }
    }
}
