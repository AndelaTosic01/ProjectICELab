using MIntermediateRepresentation.RVSDG.DataDeclarations;
using MIntermediateRepresentation.RVSDG.Types;
using MIntermediateRepresentation.RVSDG;
using MIntermediateRepresentation.RVSDG.Exceptions;

namespace MIntermediateRepresentation.RVSDG.Expression
{
    public class Sum : Operation
    {
        private readonly static string _name = "Sum";
        public override string Name => _name;

        public Sum(IOutput[] output, IInput[] input) : base(output, input)
        {
            if (input == null || input.Length < 2)
                throw new IrregularInputException("Input must contain at least 2 elements.");

            if (output[0] == null || output.Length != 1 || ((Edge)output[0]).Type is not (NumericType or StringType))
                throw new IrregularOutputException("Output variable must be of type NumericType or StringType.");
            
            CheckInputTypeCompatibility();
        }

        public override void CalcResult()
        {
            IDataType type = ((Edge)Output[0]).Type;
            if (type is StringType)
            {
                var sum = StringType.GetDefaultValue();
                foreach (var input in Inputs)
                {
                    sum += StringType.CastValue(input.GetValue());
                }
                Output[0].SetValue(sum);

            }
            else if (type is IntegerType)
            {
                var sum = IntegerType.GetDefaultValue();
                foreach (var input in Inputs)
                {
                    sum += IntegerType.CastValue(input.GetValue());
                }
                Output[0].SetValue(sum);
            }
            else if (type is RealType)
            {
                var sum = RealType.GetDefaultValue();
                foreach (var input in Inputs)
                {
                    sum += RealType.CastValue(input.GetValue());
                }
                Output[0].SetValue(sum);
            }
        }
    }
 }

