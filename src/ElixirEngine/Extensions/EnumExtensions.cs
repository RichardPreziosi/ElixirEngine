using System;
using System.Linq;

namespace ElixirEngine.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum GetMaximum<TEnum>()
            where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Max();
        }
    }
}