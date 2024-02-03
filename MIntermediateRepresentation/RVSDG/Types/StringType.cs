using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Types
{
    public class StringType : DataType
    {
        protected override int TypeId => 3;

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(StringType))
                return false;

            return TypeId == ((StringType)obj).TypeId;
        }

        public override int GetHashCode()
        {
            return TypeId;
        }

        public override bool IsAssignable(object? value)
        {
            return value?.GetType() == typeof(string);
        }

        public override bool IsCompatible(IDataType type)
        {
            return type is IntegerType or RealType or StringType;
        }

        public override string? ToString()
        {
            return $"Type={Name}, TypeId={TypeId}";
        }
        
        public static string CastValue(object? value)
        {
            return Convert.ToString(value, new CultureInfo("en-US", false )) ?? string.Empty;
        }

        public static string GetDefaultValue()
        {
            return "";
        }
    }
}
