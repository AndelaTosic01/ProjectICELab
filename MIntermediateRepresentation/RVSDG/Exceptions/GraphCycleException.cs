using MIntermediateRepresentation.RVSDG.DataDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Exceptions
{
    public class GraphCycleException : Exception
    {
        public GraphCycleException() : base("This graph has cycle and topological sort cannot be made.") { }
        public GraphCycleException(string message) : base(message) { }
    }
}
