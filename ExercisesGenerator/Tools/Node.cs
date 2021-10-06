using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Node
    {
        public Unit Value;

        public Node LeftChild;

        public Node RightChild;

        public Node()
        {
            LeftChild = null;
            RightChild = null;
        }

        public Node(Unit Value)
        {
            this.Value = Value;
            LeftChild = null;
            RightChild = null;
        }

        /// <summary>
        /// 基于括号表示法输出字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString()
                + "(" + ((LeftChild != null) ? LeftChild.ToString() : "") + ")("
                    + ((RightChild != null) ? RightChild.ToString() : "") + ")";
        }

        /// <summary>
        /// 基于层次遍历与node比较
        /// </summary>
        /// <param name="node"></param>
        /// <returns>负值 => 小于; 0 => 等于; 正值 => 大于</returns>
        public int CompareTo(Node node)
        {
            if (node == null)
            {
                return 1;
            }//node为空
            if (Value.CompareTo(node.Value) == 0)
            {
                Queue<Node> queue = new Queue<Node>();
                Queue<Node> queue2 = new Queue<Node>();
                queue.Enqueue(this);
                queue2.Enqueue(node);
                while (queue.Count != 0 && queue2.Count != 0)
                {
                    Node p = queue.Dequeue();
                    if (p.LeftChild != null)
                    {
                        queue.Enqueue(p.LeftChild);
                    }//p的左子树不空
                    if (p.RightChild != null)
                    {
                        queue.Enqueue(p.RightChild);
                    }//p的右子树不空

                    Node q = queue2.Dequeue();
                    if (q.LeftChild != null)
                    {
                        queue2.Enqueue(q.LeftChild);
                    }//q的左子树不空
                    if (q.RightChild != null)
                    {
                        queue2.Enqueue(q.RightChild);
                    }//q的右子树不空

                    if (p.Value.CompareTo(q.Value) != 0)
                    {
                        return p.Value.CompareTo(q.Value);
                    }//p和q的根节点不同
                }
                if (queue.Count != queue2.Count)
                {
                    return queue.Count - queue2.Count;
                }//剩余元素数量不同
                else
                {
                    return 0;
                }//剩余元素数量相同
            }//根节点相同
            else
            {
                return Value.CompareTo(node.Value);
            }//根节点不同
        }
    }
}
