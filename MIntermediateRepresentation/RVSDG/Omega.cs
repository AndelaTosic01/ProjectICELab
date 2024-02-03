

namespace MIntermediateRepresentation.RVSDG
{
    public class Omega : Region
    {
        private static readonly string _name = "Omega";
        public override string Name => _name;
        public Omega(List<Node> nodesInDelta, IOutput[] output, IInput[] inputs) : base(nodesInDelta, output, inputs) { }

    }
}
