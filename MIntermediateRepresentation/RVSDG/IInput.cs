using MIntermediateRepresentation.RVSDG.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG
{
    public interface IInput
    {
        public Node? NodeSrc { get; }
        public List<Node>? NodeDst { get; }
        public object GetValue();
        public void AddNodeDst(Node node);
        public void AddNodeSrc(Node node);


    }
}
