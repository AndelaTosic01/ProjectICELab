using MIntermediateRepresentation.RVSDG.DataDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Exceptions
{
    public class MissingRegionDataException : Exception
    {
        public MissingRegionDataException() : base("Region has to have input and output.") { }
        public MissingRegionDataException(string message) : base(message) { }

        public MissingRegionDataException(object? constValue) : base(String.Format("{0} is a constant.", constValue)) { }
    }
}
