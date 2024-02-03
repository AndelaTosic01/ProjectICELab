using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public class And : Operation
    {
       private readonly static string _name = "AND";
       public override string Name => _name;

        public And(IOutput[] output, IInput[] inputs) : base(output, inputs)
        {
            if (inputs.Length < 2 || inputs == null)
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
                var and = BooleanType.CastValue(Inputs[0].GetValue());
                for (int i = 1; i < Inputs.Count; i++)
                {
                    and &= BooleanType.CastValue(Inputs[i].GetValue());
                }
                Output[0].SetValue(and);
                
            }
            else if (type is IntegerType)
            {
                var and = IntegerType.CastValue(Inputs[0].GetValue());

                for (int i = 1; i < Inputs.Count; i++)
                {
                    and &= IntegerType.CastValue(Inputs[i].GetValue());
                }

                Output[0].SetValue(and);

            }
        }
    }
}
