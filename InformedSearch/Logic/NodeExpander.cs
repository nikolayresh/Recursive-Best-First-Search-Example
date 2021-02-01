using System.Collections.Generic;

namespace InformedSearch.Logic
{
    public static class NodeExpander
    {
        public static HashSet<Node> Expand(Node node)
        {
            var successors = new HashSet<Node>();
            var state = node.State;

            if (state.SmallBucket.IsEmpty())
            {
                var ps = new ProblemState(node.State);
                ps.SmallBucket.FillUp();
                successors.Add(new Node(ps, node));
            }

            if (state.BigBucket.IsEmpty())
            {
                var ps = new ProblemState(node.State);
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
                ps.SmallBucket.DumpWater();
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
                ps.BigBucket.DumpWater();
                successors.Add(new Node(ps, node));
            }

            var parentNodes = node.GetParentNodes();
            successors.RemoveWhere(s => parentNodes.Contains(s));

            return successors;
        }
    }
}