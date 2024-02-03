using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public class Sub : Operation
    {
        private readonly static string _name = "Subtraction";
        public override string Name => _name;

        public Sub(IOutput[] output, IInput[] input) : base(output, input)
        {
            if (input[0] == null || input.Length < 2)
                throw new IrregularInputException("Input must contain at least 2 elements");

            if (output[0] == null || output.Length != 1 || ((Edge)output[0]).Type is not NumericType)
                throw new IrregularOutputException("Output variable must be of type NumericType");

            CheckInputTypeCompatibility();
        }

        public override void CalcResult()
        {
            IDataType type = ((Edge)Output[0]).Type;
            if (type is IntegerType)
            {
                var sub = IntegerType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    sub -= IntegerType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(sub);
            }
            else if (type is RealType)
            {
                //var sub = RealType.GetDefaultValue();
                var sub = RealType.CastValue(Inputs[0].GetValue());
                for (int i=1; i<Inputs.Count; i++)
                {
                    sub -= RealType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(sub);
            }
        }
    }
}
