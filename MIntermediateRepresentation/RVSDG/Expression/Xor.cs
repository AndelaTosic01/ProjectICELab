using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public class Xor : Operation
    {
        private readonly static string _name = "XOR";
        public override string Name => _name;

        public Xor(IOutput[] output, IInput[] input) : base(output, input)
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
                var xor = BooleanType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    xor ^= BooleanType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(xor);
            }
            else if (type is IntegerType)
            {
                var xor = IntegerType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    xor ^= IntegerType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(xor);
            }
        }
    }
}
