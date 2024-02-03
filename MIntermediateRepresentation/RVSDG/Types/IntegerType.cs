using System.Globalization;

namespace MIntermediateRepresentation.RVSDG.Types
{
    public class IntegerType : NumericType
    {
        protected override int TypeId => 0;

        public IntegerType() { }


        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(IntegerType))
            {
                return false;
            }

            return ((IntegerType)obj).TypeId == TypeId;
        }

        public override int GetHashCode()
        {
            return TypeId;
        }

        public override bool IsAssignable(object? value)
        {
            return value?.GetType() == typeof(int) || value?.GetType() == typeof(long);
        }

        public override bool IsCompatible(IDataType type)
        {
            return type is IntegerType or RealType or StringType or BooleanType; 
        }

        public override string? ToString()
        {
            return $"Type={Name}, TypeId={TypeId}";
        }

        public static long CastValue(object? value)
        {
            if (value is string)
            {
                Int64.TryParse(value as string, NumberStyles.Number, new CultureInfo("en-US", false ), out var result);
                return result;
            }

            return Convert.ToInt64(value,new CultureInfo("en-US", false ));
        }

        public static long GetDefaultValue()
        {
            return 0L;
        }
    }
}
