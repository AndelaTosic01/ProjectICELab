using MIntermediateRepresentation.RVSDG.Types;

namespace MIntermediateRepresentation.RVSDG.DataDeclarations
{
    public abstract class DataDeclaration
    {
        public abstract string Name { get; }
        public abstract object Value { get; set; }
        public abstract IDataType Type { get; }
    }
}
