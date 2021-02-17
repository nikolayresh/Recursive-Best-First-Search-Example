using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InformedSearch.Logic
{
    /// <summary>
    /// Node of a problem state
    /// </summary>
    public sealed class Node
    {
        public Node(ProblemState state, Node parent)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
            Parent = parent;
        }

        public Node(ProblemState state) 
            : this(state, null)
        {
        }

        /// <summary>
        /// Returns state of the problem
        /// </summary>
        public ProblemState State
        {
            get;
        }

        /// <summary>
        /// Returns the parent node
        /// </summary>
        public Node Parent
        {
            get;
        }

        /// <summary>
        /// Returns a boolean value whether this node is the ROOT node
        /// </summary>
        public bool IsRoot()
        {
            return Parent == null;
        }

        /// <summary>
        /// Expands this node to provide new states
        /// </summary>
        public HashSet<Node> Expand()
        {
            return NodeExpander.Expand(this);
        }

        /// <summary>
        /// Returns all parents of this node
        /// </summary>
        public HashSet<Node> GetParentNodes()
        {
            HashSet<Node> parents = new HashSet<Node>();
            Node node = Parent;

            while (node != null)
            {
                parents.Add(node);
                node = node.Parent;
            }

            return parents;
        }

        /// <summary>
        /// Get level of this node in relation to the root node
        /// </summary>
        public int GetLevel()
        {
            int level = 0;
            Node node = Parent;

            while (node != null)
            {
                level++;
                node = node.Parent;
            }

            return level;
        }

        public string PathFromRoot()
        {
            HashSet<ProblemState> states = new HashSet<ProblemState> { State };
            Node node = Parent;

            while (node != null)
            {
                states.Add(node.State);
                node = node.Parent;
            }

            states = states.Reverse().ToHashSet();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < states.Count; i++)
            {
                ProblemState nextState = states.ElementAt(i);
                sb.Append(nextState);

                if (i + 1 != states.Count)
                {
                    sb.Append("->");
                }
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            Node otherNode = (Node) obj;

            return Equals(State, otherNode.State);
        }

        public override int GetHashCode()
        {
            return HashUtility.GetHashCode(State);
        }

        public override string ToString()
        {
            return $"Level: {GetLevel()}; State: {State}";
        }
    }
}