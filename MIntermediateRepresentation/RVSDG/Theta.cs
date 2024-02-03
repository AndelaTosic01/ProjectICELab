namespace MIntermediateRepresentation.RVSDG
{
    public class Theta : Region
    {
        private static readonly string _name = "Theta";
        public override string Name => _name;
        public Theta(List<Node> nodesInDelta, IOutput[] output, params IInput[] inputs) : base(nodesInDelta, output, inputs) { }
    }
}
