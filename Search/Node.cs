// https://projectbalint.com/en/page/uninformed-search.html
// Node.cs
//
// An arbitrary node of a search problem
// Copyright (c) 2018 Balint Gyevnar

namespace UninformedSearch.Search
{
    /// <summary>
    /// Class structure to represent an arbitrary node in the search tree
    /// </summary>
    class Node
    {
        /// <summary>
        /// A static readonly Node to represent the state of failure
        /// </summary>
        public static readonly Node FAILURE = new Node()
        {
            PathCost = 0,
            Parent = null,
            State = null,
            Action = null
        };

        /// <summary>
        /// A static readonly node to represent a cutoff in DLS
        /// </summary>
        public static readonly Node CUTOFF = new Node()
        {
            PathCost = -1,
            Parent = null,
            State = null,
            Action = null
        };

        /// <summary>
        /// The total cost so far to reach this node
        /// </summary>
        public int PathCost { get; set; }

        /// <summary>
        /// The parent node of the current node
        /// </summary>
        public Node Parent { get; set; }

        /// <summary>
        /// The state representation of corresponding to the current node
        /// </summary>
        public IState State { get; set; }

        /// <summary>
        /// The action that resulted in the current node
        /// </summary>
        public IAction Action { get; set; }

        /// <summary>
        /// Construct a child node
        /// </summary>
        /// <param name="p">The problem environment</param>
        /// <param name="a">The action to apply for the current node</param>
        /// <returns>A new node resulting from applying the given action for the current node</returns>
        public virtual Node ChildNode(IProblem p, IAction a)
        {
            Node childNode = new Node
            {
                PathCost = PathCost + p.StepCost(State, a),
                Parent = this,
                State = p.Result(State, a),
                Action = a
            };

            return childNode;
        }

        public override bool Equals(object obj)
        {
            if (obj is Node n)
            {
                return PathCost == n.PathCost &&
                    Parent == n.Parent &&
                    State == n.State &&
                    Action == n.Action;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int prime = 29;
            int parent = Parent == null ? 0 : Parent.GetHashCode();
            int action = Action == null ? 0 : Action.GetHashCode();
            return parent +
                prime * (PathCost.GetHashCode() +
                prime * (State.GetHashCode() +
                prime * action));
        }
    }
}
