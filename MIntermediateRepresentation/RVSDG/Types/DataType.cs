using MIntermediateRepresentation.RVSDG.DataDeclarations;

namespace MIntermediateRepresentation.RVSDG.Types
{
    public abstract class DataType : IDataType
    {
        public string Name { get => this.GetType().Name; }

        private static readonly List<DataType> _types = new();

        protected abstract int TypeId { get; }

        static DataType()
        {
            //load all subTypes
            List<System.Type> subTypes = typeof(DataType).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(DataType))).ToList();
            Dictionary<int, DataType> types = new();

            foreach (var subType in subTypes)
            {
                var constructor = subType.GetConstructor(System.Type.EmptyTypes);
                if (constructor == null)
                {
                    continue;
                }
                DataType type = (DataType)constructor.Invoke(System.Type.EmptyTypes);
                types[type.TypeId] = type;
            }
            _types.AddRange(types.Values);
        }

        public static IDataType? GetType(object? value)
        {
            IDataType? type = null;
            foreach (var subType in _types)
            {
                if (subType.IsAssignable(value))
                {
                    type = subType;
                    break;
                }
            }
            return type;
        }

        public abstract object Clone();

        public abstract bool IsCompatible(IDataType type);

        public abstract bool IsAssignable(object? value);

        public abstract override string? ToString();

        public abstract override int GetHashCode();

        public abstract override bool Equals(object? obj);

        public static object GetDefaultValue(IDataType type)
        {
            if (type is IntegerType) return IntegerType.GetDefaultValue();
            if (type is RealType) return RealType.GetDefaultValue();
            if (type is BooleanType) return BooleanType.GetDefaultValue();
            if (type is StringType) return StringType.GetDefaultValue();
            
            throw new ArgumentException($"ValueType {type} not supported");
        }
        
        public static object CastValue(IDataType type, object value)
        {
            if (type is IntegerType) return IntegerType.CastValue(value);
            if (type is RealType) return RealType.CastValue(value);
            if (type is BooleanType) return BooleanType.CastValue(value);
            if (type is StringType) return StringType.CastValue(value);
            
            throw new ArgumentException($"ValueType {type} not supported");
        }
    }
}
