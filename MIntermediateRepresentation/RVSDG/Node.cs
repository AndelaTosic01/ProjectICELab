using MIntermediateRepresentation.RVSDG.Exceptions;
namespace MIntermediateRepresentation.RVSDG
{
    public abstract class Node : INode
    {
        private readonly List<IInput> _inputs = new();
        private readonly List<IOutput> _output = new();
        public List<IInput> Inputs { get => _inputs; }
        public List<IOutput> Output { get => _output; }
        public abstract string Name { get; }

        public Node(IOutput[] output, IInput[] inputs)
        {
            _output = output.ToList();
            _inputs = inputs.ToList();

            _inputs.ForEach(x => x.AddNodeDst(this));
            _output.ForEach(x => x.AddNodeSrc(this));
        }



    }
}
