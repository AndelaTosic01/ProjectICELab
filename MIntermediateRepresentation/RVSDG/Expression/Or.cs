using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public class Or : Operation
    {
        private readonly static string _name = "OR";
        public override string Name => _name;
        public Or(IOutput[] output, IInput[] input) : base(output, input)
        {
            if (input == null || input.Length < 2)
                throw new IrregularInputException("Input must contain at least 2 elements.");

            if (output == null || output.Length != 1 || ((Edge)output[0]).Type is not (IntegerType or BooleanType))
                throw new IrregularOutputException("Output variable must be of type IntegerType or BooleanType.");
            
            CheckInputTypeCompatibility();
        }
        public override void CalcResult()
        {
            IDataType type = ((Edge)Output[0]).Type;
            if (type is BooleanType)
            {
                var or = BooleanType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    or |= BooleanType.CastValue(Inputs[i].GetValue());
                    if (or)
                    {
                        break;
                    }
                }
                Output[0].SetValue(or);
            }
            else if (type is IntegerType)
            {
                var or = IntegerType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    or |= IntegerType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(or);
            }
        }
    }
}