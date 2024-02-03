using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;

namespace MIntermediateRepresentation.RVSDG
{
    public class Edge : IInput, IOutput
    {
        private readonly DataDeclaration _dataDeclaraion;
        private Node? _nodeSrc;
        private readonly List<Node> _nodeDst = new();

        public Node? NodeSrc { get => _nodeSrc;  }
        public List<Node>? NodeDst { get => _nodeDst;  }
        public string Name { get => _dataDeclaraion.Name ; }
        public IDataType Type { get => _dataDeclaraion.Type ; }
        public object Value { get => _dataDeclaraion.Value ; }

        public Edge (DataDeclaration _dataDeclaration)
        {
            this._dataDeclaraion = _dataDeclaration ?? throw new ArgumentException("Data declaration cannot be null");
        }
        public object GetValue()
        {
            return _dataDeclaraion.Value;
        }
        public void SetValue(object Value)
        {
            _dataDeclaraion.Value = Value;
        }

        public void AddNodeDst(Node node)
        {
            _nodeDst.Add(node);
        }
        public void AddNodeSrc(Node node)
        {
            if (_nodeSrc == null)
                _nodeSrc = node;
        }
    }
}
