using MIntermediateRepresentation.RVSDG.Types;

namespace MIntermediateRepresentation.RVSDG.DataDeclarations
{
    public class Variable : DataDeclaration
    {
        private readonly string _name;
        public override string Name => _name;

        private object _value;
        public override object Value { get => _value; set => _value = value; }

        private readonly IDataType _type;
        public override IDataType Type => _type;

        public Variable(string name, object value) : this(name, null, value) { }

        public Variable(string name, IDataType? type, object? value = null)
        {

            if (type == null && value != null)
            {
                type = DataType.GetType(value);
                
                if(type == null) throw new ArgumentException("Value " + value.GetType() + " not supported.");
            }else if (value == null && type != null)
            {
                value = DataType.GetDefaultValue(type);
            }
            else if(value != null && type != null)
            {
                value = DataType.CastValue(type, value);
            }
            else
            {
                throw new ArgumentException($"Missing type and value for variable {name}");
            }

            _name = name;
            _type = type;
            _value = value;

        }
    }
}
