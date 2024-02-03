namespace MIntermediateRepresentation.RVSDG
{
    public class Lambda : Region
    {
        private readonly static string _name = "Lambda";
        public override string Name => _name;
        public Lambda(List<Node> nodesInDelta, IOutput[] output, IInput[] inputs) : base(nodesInDelta, output, inputs) { }

    }
}
