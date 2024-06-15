using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberManager
{
    public class TreeOfRangeNumberManager : INumberManager
    {
        private RangeNode root = RangeNode.New(new(0, int.MaxValue));

        public int GetNumber()
        {
            var spine = new Stack<(RangeNode, Direction)>();
            var n = root;
            while (n.LeftChild != null)
            {
                spine.Push((n, Direction.Left));
                n = n.LeftChild;
            }

            var res = n.Range.Start;
            if (n.Range.Length > 1)
            {
                n = RangeNode.New(new(n.Range.Start + 1, n.Range.Length - 1), n.LeftChild, n.RightChild);
            }
            else
            {
                if (n.RightChild != null)
                {
                    n = RangeNode.New(n.RightChild.Range, null, n.RightChild.RightChild);
                }
                else
                {
                    n = null;
                }
            }

            root = Respine(spine, n);
            return res;
        }

        public void ReleaseNumber(int number)
        {
            var spine = new Stack<(RangeNode, Direction)>();
            var n = root;

            while (n != null)
            {
                if (number + 1 == n.Range.Start)
                {
                    // TODO: Coalesce adjacent nodes if possible.
                    n = RangeNode.New(new Range(number, n.Range.Length + 1), n.LeftChild, n.RightChild);
                    break;
                }
                else if (number == n.Range.Start + n.Range.Length)
                {
                    // TODO: Coalesce adjacent nodes if possible.
                    n = RangeNode.New(new Range(n.Range.Start, n.Range.Length + 1), n.LeftChild, n.RightChild);
                    break;
                }
                else
                {
                    if (number < n.Range.Start)
                    {
                        spine.Push((n, Direction.Left));
                        n = n.LeftChild;
                    }
                    else
                    {
                        spine.Push((n, Direction.Right));
                        n = n.RightChild;
                    }
                }
            }

            if (n == null)
            {
                n = RangeNode.New(new(number, 1));
            }

            root = Respine(spine, n);
        }

        private static RangeNode Respine(Stack<(RangeNode, Direction)> spine, RangeNode? n)
        {
            // TODO: Figure out how to keep the tree balanced
            while (spine.Any())
            {
                var (p, d) = spine.Pop();
                if (d == Direction.Left)
                {
                    n = RangeNode.New(p.Range, n, p.RightChild);
                }
                else
                {
                    n = RangeNode.New(p.Range, p.LeftChild, n);
                }
            }

            if (n == null)
            {
                throw new InvalidOperationException();
            }

            return n;
        }

        private enum Direction
        {
            Left = -1,
            Right = 1,
        }

        [DebuggerDisplay("{Range}")]
        private abstract class RangeNode
        {
            public abstract RangeNode? LeftChild { get; }

            public abstract RangeNode? RightChild { get; }

            public Range Range { get; }

            protected RangeNode(Range range)
            {
                this.Range = range;
            }

            public static RangeNode New(Range range, RangeNode? leftChild = null, RangeNode? rightChild = null)
            {
                if (leftChild == null && rightChild == null)
                {
                    return new LeafNode(range);
                }
                else
                {
                    return new InteriorNode(range, leftChild, rightChild);
                }
            }

            private class LeafNode : RangeNode
            {
                public LeafNode(Range range)
                    : base(range)
                {
                }

                public override RangeNode? LeftChild => null;
                public override RangeNode? RightChild => null;
            }

            private class InteriorNode : RangeNode
            {
                public InteriorNode(Range range, RangeNode? leftChild, RangeNode? rightChild)
                    : base(range)
                {
                    LeftChild = leftChild;
                    RightChild = rightChild;
                }

                public override RangeNode? LeftChild { get; }
                public override RangeNode? RightChild { get; }
            }
        }
    }
}
