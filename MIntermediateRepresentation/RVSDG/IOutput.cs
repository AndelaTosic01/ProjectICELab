using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG
{
    public interface IOutput
    {
        public Node? NodeSrc { get ; }
        public List<Node>? NodeDst { get ; }
        public void SetValue(object Value);
        public void AddNodeDst(Node node); //??livello di protezione quale
        public void AddNodeSrc(Node node);  //??livello di protezione quale
    }
}
