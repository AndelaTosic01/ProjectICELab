using MIntermediateRepresentation.RVSDG.DataDeclarations;
namespace MIntermediateRepresentation.RVSDG.Expression;

public abstract class Operation : Node
{
    public override abstract string Name { get; }

    public Operation(IOutput[] output, IInput[] inputs) : base(output, inputs){
    }

    public abstract void CalcResult();

    protected void CheckInputTypeCompatibility()
    {
        foreach (IInput d in Inputs)
        {
            if (!((Edge)d).Type.IsCompatible(((Edge)Output[0]).Type))
            {
                
                throw new ArgumentException($"Input {((Edge)d).Name} of type {((Edge)d).Type} must be compatible with output variable {((Edge)Output[0]).Name} of type {((Edge)Output[0]).Type}.");
            }
        }
    }
}
