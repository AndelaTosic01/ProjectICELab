namespace MIntermediateRepresentation.RVSDG
{
    public class Gamma : Region
    {
        private readonly static string _name = "Gamma";
        public override string Name => _name;

        public Gamma(List<Node> nodesInDelta, IOutput[] output, IInput[] inputs) : base(nodesInDelta, output, inputs) { }

    }
}
