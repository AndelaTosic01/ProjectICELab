namespace MIntermediateRepresentation.RVSDG.Types
{
    public interface IDataType : ICloneable
    {
        public bool IsCompatible(IDataType type);

        public bool IsAssignable(object? value);
        
    }
}
