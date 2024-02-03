using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIntermediateRepresentation.RVSDG
{
    public static class RegionFactory
    {
        public static Region CreateReagion(string regionName, List<Node> nodes, IOutput[] outputs, IInput[] inputs)
        {
            if (regionName.Equals("Omega", StringComparison.OrdinalIgnoreCase))
                return new Omega(nodes, outputs, inputs);
            else if (regionName.Equals("Delta", StringComparison.OrdinalIgnoreCase))
                return new Delta(nodes, outputs, inputs);
            else if (regionName.Equals("Gamma", StringComparison.OrdinalIgnoreCase))
                return new Gamma(nodes, outputs, inputs);
            else if (regionName.Equals("Lambda", StringComparison.OrdinalIgnoreCase))
                return new Lambda(nodes, outputs, inputs);
            else if (regionName.Equals("Theta", StringComparison.OrdinalIgnoreCase))
                return new Theta(nodes, outputs, inputs);
            else
                throw new ArgumentException("This type of region doesn't exist yet.");
        }

    }
}
