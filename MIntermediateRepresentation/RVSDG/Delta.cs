namespace MIntermediateRepresentation.RVSDG
{
    public class Delta : Region
    {
        private readonly static string _name = "Delta";
        public override string Name => _name;

        public Delta(List<Node> nodesInDelta, IOutput[] output, IInput[] inputs) : base(nodesInDelta, output, inputs) {}
    }
}
