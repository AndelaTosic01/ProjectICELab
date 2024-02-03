using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Types
{
    public class RealType : NumericType
    {
        protected override int TypeId => 1;

        public RealType() { }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(RealType))
                return false;

            return TypeId == ((RealType)obj).TypeId;
        }

        public override int GetHashCode()
        {
            return TypeId;
        }

        public override bool IsAssignable(object? value)
        {
            return value?.GetType() == typeof(float) || value?.GetType() == typeof(double);
        }

        public override bool IsCompatible(IDataType type)
        {
            return type is IntegerType or RealType or StringType or BooleanType;
        }

        public override string? ToString()
        {
            return $"Type={Name}, TypeId={TypeId}";
        }

        public static double CastValue(object? value)
        {
            if (value is string)
            {
                double.TryParse(value as string, NumberStyles.Number, new CultureInfo("en-US", false ), out var result);
                return result;
            }else if (value is float)
            {
                //cut precision
                return Math.Round((float)value, 7);
            }

            return Convert.ToDouble(value, new CultureInfo("en-US", false ));
        }

        
        public static double GetDefaultValue()
        {
            return 0.0;
        }
    }
}
