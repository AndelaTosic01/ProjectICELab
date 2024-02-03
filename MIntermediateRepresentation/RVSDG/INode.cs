namespace MIntermediateRepresentation.RVSDG
{
    public interface INode
    {
        public List<IInput> Inputs { get; }
        public List<IOutput> Output { get; }
        public string Name { get; }
    }
}
