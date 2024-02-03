using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public class Min : Operation
    {
        private static readonly string _name = "Min";
        public override string Name => _name;

        public Min(IOutput[] output, IInput[] input) : base(output, input)
        {
            if (input == null || input.Length < 2)
                throw new IrregularInputException("Input must contain at least 2 elements");

            if (output == null || output.Length != 1 || ((Edge)output[0]).Type is not NumericType)
                throw new IrregularOutputException("Output variable must be of type NumericType");

            CheckInputTypeCompatibility();
        }

        public override void CalcResult()
        {
            IDataType type = ((Edge)Output[0]).Type;
            if (type is IntegerType)
            {
                var minValue = IntegerType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    if (IntegerType.CastValue(Inputs[i].GetValue()) < minValue)
                        minValue = IntegerType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(minValue);
            }
            else if (type is RealType)
            {
                var minValue = RealType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    if (RealType.CastValue(Inputs[i].GetValue()) < minValue)
                        minValue = RealType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(minValue);
            }
        }
    }
}
