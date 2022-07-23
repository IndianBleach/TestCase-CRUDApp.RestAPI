using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Domain.Enums
{
    public static class EnumValueHelper
    {
        public static T GetEnumName<T>(int intValue) where T : struct
        {
            if (typeof(T).IsEnum)
            {
                T val = ((T[])Enum.GetValues(typeof(T)))[0];

                foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
                {
                    if (Convert.ToInt32(enumValue).Equals(intValue))
                    {
                        val = enumValue;
                        break;
                    }
                }
                return val;
            }
            else return default(T);
        }
    }
}
