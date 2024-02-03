using MIntermediateRepresentation.RVSDG.Exceptions;
using MIntermediateRepresentation.RVSDG.Types;

namespace MIntermediateRepresentation.RVSDG.DataDeclarations
{
    public class Constant : DataDeclaration
    {
        private readonly string _name;
        private readonly object _value;
        private readonly IDataType _type;
        public override string Name => _name;
        public override IDataType Type => _type;
        public override object Value 
        { 
            get => _value;
            set => throw new ChangingConstantException(_value);
        }

        public Constant(string name, object value) : this(name, null, value)
        {
            var type = Types.DataType.GetType(value);
            if (type == null)
            {
                throw new ArgumentException("ValueType " + value.GetType() + " not supported.");
            }
            
            _name = name;
            _type = type;
            _value = value;
        }

        public Constant(string name, IDataType type, object value)
        {
            _name = name;
            _type = type;
            _value = DataType.CastValue(type,value);
        }
    }
}
