using MIntermediateRepresentation.RVSDG.DataDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public static class OperationsFactory 
    {
        public static Operation MakeOperation(string operation, IOutput[] output, IInput[] input)

        {
            if (operation.Equals("Div", StringComparison.OrdinalIgnoreCase))
            {
                return new Div(output, input);
            }
            else if (operation.Equals("Sum", StringComparison.OrdinalIgnoreCase))
            {
                return new Sum(output, input);
            }
            else if (operation.Equals("Mul", StringComparison.OrdinalIgnoreCase))
            {
                return new Mul(output, input);
            }
            else if (operation.Equals("Sub", StringComparison.OrdinalIgnoreCase))
            {
                return new Sub(output, input);
            }
            else if (operation.Equals("Min", StringComparison.OrdinalIgnoreCase))
            {
                return new Min(output, input);
            }
            else if (operation.Equals("Max", StringComparison.OrdinalIgnoreCase))
            {
                return new Max(output, input);
            }
            else if (operation.Equals("or", StringComparison.OrdinalIgnoreCase))
            {
                return new Or(output, input);
            }
            else if (operation.Equals("and", StringComparison.OrdinalIgnoreCase))
            {
                return new And(output, input);
            }
            else if (operation.Equals("not", StringComparison.OrdinalIgnoreCase))
            {
                return new Not(output, input);
            }
            else if (operation.Equals("xor", StringComparison.OrdinalIgnoreCase))
            {
                return new Xor(output, input);
            }
            else 
                throw new ArgumentException(operation + " is not supported yet");
        }
    }
}
