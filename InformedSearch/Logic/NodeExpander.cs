using System.Collections.Generic;

namespace InformedSearch.Logic
{
    /// <summary>
    /// Class that expands nodes
    /// </summary>
    public static class NodeExpander
    {
        public static HashSet<Node> Expand(Node node)
        {
            HashSet<Node> successors = new HashSet<Node>();
            ProblemState state = node.State;

            if (state.SmallBucket.IsEmpty())
            {
                ProblemState ps = new ProblemState(node.State);
                ps.SmallBucket.FillUp();
                successors.Add(new Node(ps, node));
            }

            if (state.BigBucket.IsEmpty())
            {
                ProblemState ps = new ProblemState(node.State);
                ps.BigBucket.FillUp();
                successors.Add(new Node(ps, node));
            }

            if (!state.SmallBucket.IsEmpty())
            {
                ProblemState ps;

                if (!state.BigBucket.IsFull())
                {
                    ps = new ProblemState(node.State);
                    ps.SmallBucket.PourWaterInto(ps.BigBucket);
                    successors.Add(new Node(ps, node));
                }

                ps = new ProblemState(node.State);
                ps.SmallBucket.Dump();
                successors.Add(new Node(ps, node));
            }

            if (!state.BigBucket.IsEmpty())
            {
                ProblemState ps;

                if (!state.SmallBucket.IsFull())
                {
                    ps = new ProblemState(node.State);
                    ps.BigBucket.PourWaterInto(ps.SmallBucket);
                    successors.Add(new Node(ps, node));
                }

                ps = new ProblemState(node.State);
                ps.BigBucket.Dump();
                successors.Add(new Node(ps, node));
            }

            HashSet<Node> parentNodes = node.GetParentNodes();
            successors.RemoveWhere(n => parentNodes.Contains(n));

            return successors;
        }
    }
}