using MIntermediateRepresentation.RVSDG.DataDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Exceptions
{
    public class ChangingConstantException : Exception
    {
        public ChangingConstantException() : base("The value is a constant and it is not possibile to modify.") { }
        public ChangingConstantException(string message) : base(message) { }

        public ChangingConstantException(object? constValue) : base(String.Format("{0} is a constant.", constValue)) {}
    }
}
