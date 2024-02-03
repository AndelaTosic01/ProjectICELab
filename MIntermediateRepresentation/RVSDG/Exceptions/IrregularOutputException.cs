using MIntermediateRepresentation.RVSDG.DataDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Exceptions
{
    public class IrregularOutputException : ArgumentException
    {
        public IrregularOutputException() : base("Output is not correct.") { }
        public IrregularOutputException(string message) : base(message) { }

        public IrregularOutputException(object? constValue) : base(String.Format("{0} is a constant.", constValue)) { }
    }
}