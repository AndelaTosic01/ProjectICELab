using MIntermediateRepresentation.RVSDG.DataDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Exceptions
{
    public class IrregularInputException : ArgumentException
    {
        public IrregularInputException() : base("Input is not correct.") { }
        public IrregularInputException(string message) : base(message) { }

        public IrregularInputException(object? constValue) : base(String.Format("{0} is a constant.", constValue)) { }
    }
}
