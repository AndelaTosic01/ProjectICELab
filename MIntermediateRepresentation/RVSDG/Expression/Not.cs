using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public class Not : Operation
    {
        private static readonly string _name = "NOT";
        public override string Name => _name;

        public Not(IOutput[] output, IInput[] input) : base(output, input)
        {
            if (input[0] == null || input.Length != 1)
                throw new IrregularInputException("Input must contains only 1 element.");

            if (output[0] == null || output.Length != 1 || ((Edge)output[0]).Type is not (IntegerType or BooleanType))
                throw new IrregularOutputException("Output variable must be of type IntegerType or BooleanType.");

            CheckInputTypeCompatibility();
        }

        public override void CalcResult()
        {
            IDataType type = ((Edge)Output[0]).Type;
            if (type is BooleanType)
            {
                Output[0].SetValue(!BooleanType.CastValue(Inputs[0].GetValue()));
            }
            if (type is IntegerType)
            {
                var val = IntegerType.CastValue(Inputs[0].GetValue());
                if (val != 0)
                    Output[0].SetValue(0);
                else
                    Output[0].SetValue(1);
            }
        }
    }
}