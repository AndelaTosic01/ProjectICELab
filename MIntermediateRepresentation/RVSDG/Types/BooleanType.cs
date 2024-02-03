using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Types
{
    public class BooleanType : DataType
    {
        protected override int TypeId => 2;

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(BooleanType))
                return false;

            return TypeId == ((BooleanType)obj).TypeId;
        }

        public override int GetHashCode()
        {
            return TypeId;
        }

        public override bool IsAssignable(object? value)
        {
            return value?.GetType() == typeof(bool);
        }

        public override bool IsCompatible(IDataType type)
        {
            return type is BooleanType || type is IntegerType || type is RealType || type is StringType;
        }

        public override string ToString()
        {
            return $"Type={Name}, TypeId={TypeId}";
        }

        public static bool CastValue(object value)
        {
            if (value is string)
            {
                throw new ArgumentException("Invalid cast from StringType to BooleanType.");
                /*bool.TryParse(value as string, out var result);
                return result;*/
            }
            
            return Convert.ToBoolean(value, new CultureInfo("en-US", false));
        }

        public static bool GetDefaultValue()
        {
            return true;
        }
    }
}
