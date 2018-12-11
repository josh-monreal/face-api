using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace FA.Business.Utilities
{
    public static class EnumerationExtensions
    {
        public static string GetDescription<T>(this T enumeration) where T : IConvertible
        {
            if (enumeration is Enum)
            {
                Type type = enumeration.GetType();
                Array values = Enum.GetValues(type);

                foreach (int value in values)
                {
                    if (value == enumeration.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memberInfos = type.GetMember(type.GetEnumName(value));

                        if (memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null;
        }
    }
}
