using MIntermediateRepresentation.RVSDG.Exceptions;
namespace MIntermediateRepresentation.RVSDG
{
    public abstract class Region : Node
    {
        public override abstract string Name { get; }
        private List<Node> _nodesInRegion = new ();
        private int[,] _adjMatrix; //adjacency matrix 
        public List<Node>? NodesInRegion => _nodesInRegion;

        private Region(IOutput[] output, params IInput[] inputs) : base(output, inputs) { }
        public Region(List<Node>? nodesInRegion, IOutput[] output, IInput[] inputs) : this(output, inputs)
        {
            if (output == null || inputs == null)
                throw new MissingRegionDataException();

            if (nodesInRegion != null)
            {
                _nodesInRegion = nodesInRegion;
                _adjMatrix = new int[_nodesInRegion.Count, _nodesInRegion.Count];
                MakeMatrix();
                TopologicalSort();
            }
        }

        public void AddNodesInRegion(List<Node> nodesInRegion)
        {
            if (_nodesInRegion.Count != 0)
                throw new ArgumentException(String.Format("{0} region has already nodes", this.Name));
            
            _adjMatrix = new int[_nodesInRegion.Count, _nodesInRegion.Count];
            _nodesInRegion.AddRange(nodesInRegion);
            MakeMatrix();
            TopologicalSort();
        }

        private void MakeMatrix()
        {
            int indexNode;

            for (int i = 0; i < _nodesInRegion.Count; i++)
            {
                foreach (IInput input in _nodesInRegion[i].Inputs)
                {
                    if (input.NodeSrc != null)
                    {
                        indexNode = _nodesInRegion.IndexOf(input.NodeSrc);
                        _adjMatrix[indexNode, i] = 1;
                    }
                }                
            }
        }

        private void TopologicalSort()
        {
            Node[] auxArray = new Node[_nodesInRegion.Count];
            int numNodes = _nodesInRegion.Count;
            int currentNode;

            while (numNodes > 0) {
                currentNode = NoSuccessors(numNodes);
                if (currentNode == -1)
                    throw new GraphCycleException();

                auxArray[numNodes - 1] = _nodesInRegion.ElementAt(currentNode);

                DeleteNode(currentNode, numNodes);
                numNodes--;
            }
            _nodesInRegion = auxArray.ToList<Node>();
        }

        private int NoSuccessors(int numNodes)
        {
            Boolean hasSucc;

            for (int row = 0; row < numNodes; row++)
            {
                hasSucc = false;
                for (int col = 0; col < numNodes; col++)
                {
                    if (_adjMatrix[row, col] == 1)
                    {
                        hasSucc = true;
                        break;
                    }
                }
                if (!hasSucc)
                    return row;
            }

            return -1; 
        }

        private void DeleteNode(int delIndex, int currentNumNodes)
        {
            //if not last row/node
            if (delIndex != currentNumNodes - 1) {
                //remove this node from nodesList of graph
                _nodesInRegion.RemoveAt(delIndex);

                //delete row from AjdMatrix
                for (int row = delIndex; row < currentNumNodes - 1; row++)
                    MoveRowUp(row, currentNumNodes);

                //delete col from ajdMatrix
                for (int col = delIndex; col < currentNumNodes - 1; col++)
                    MoveColLeft(col, currentNumNodes - 1);
            }
        }

        private void MoveRowUp(int row, int currentNumNodes) {
            for (int col = 0; col < currentNumNodes; col++)
                _adjMatrix[row, col] = _adjMatrix[row+1,col];
        
        }

        private void MoveColLeft(int col,int currentNumNodes) {
            for (int row=0; row < currentNumNodes; row++)
                _adjMatrix[row, col] = _adjMatrix[row, col+1];
        }
    }
}
