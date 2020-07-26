using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;

namespace LeetCode2019.ByOrder
{
    public class Top101_150
    {
        //==========================DFS====================================================//
        //----------101. Symmetric Tree--Easy----------------------------------------------//
        /*
            Given a binary tree, check whether it is a mirror of itself (ie, symmetric around its center).

            For example, this binary tree [1,2,2,3,4,4,3] is symmetric:

                1
               / \
              2   2
             / \ / \
            3  4 4  3
            

            But the following [1,2,2,null,3,null,3] is not:

                1
               / \
              2   2
               \   \
                3    3
         */
        /// <summary>
        /// DFS check if left.left mirrors right.right, left.right mirrors right.left
        /// Time Complexity: O(n) Space Complexity: O(h)=O(n) Stack space 
        /// </summary>
        public bool IsSymmetricRecursion(TreeNode root) 
        {
            return IsSymme(root, root);
        }
        public bool IsSymme(TreeNode node1, TreeNode node2 )
        {
            if(node1==null && node2==null)
                return true;
            if(node1==null || node2==null)
                return false;
            if(node1.val!=node2.val)
                return false;
            bool outter = IsSymme(node1.left, node2.right);
            bool inner = IsSymme(node1.right, node2.left);
            return outter&& inner;
            
        }

        /// <summary>
        /// BFS use queue
        /// </summary>
        public bool IsSymmetric(TreeNode root) 
        {
            if(root==null)
                return true;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root.left);
            q.Enqueue(root.right);
            while(q.Count>0)
            {
                TreeNode node1 = q.Dequeue();
                TreeNode node2 = q.Dequeue();
                
                if(node1==null && node2==null)
                    continue;
                if(node1==null|| node2==null)
                    return false;
                if(node1.val !=node2.val)
                    return false;
                q.Enqueue(node1.left);
                q.Enqueue(node2.right);
                q.Enqueue(node1.right);
                q.Enqueue(node2.left);
            }
            return true;
        }

        //========================================================================================//
        //----------103. Binary Tree Zigzag Level Order Traversal--Medium-------------------------//
        /*
        Given a binary tree, return the zigzag level order traversal of its nodes' values. (ie, from left to right, then right to left for the next level and alternate between).

            For example:
            Given binary tree [3,9,20,null,null,15,7],
                3
            / \
            9  20
                /  \
            15   7
            return its zigzag level order traversal as:
            [
            [3],
            [20,9],
            [15,7]
            ]
        */
        /// <summary>
        /// Solution1: BFS
        /// Time Complexity: O(n), Space Complexity: O(largest count of one level)
        /// </summary>
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root) {
            IList<IList<int>> res = new List<IList<int>>();
            if(root ==null)
                return res;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            bool even = false;
            while(queue.Count>0)
            {
                IList<int> level = new List<int>();
                int size = queue.Count;
                for(int i = 0; i<size; i++)
                {
                    TreeNode curNode = queue.Dequeue();
                    if(even)
                        level.Insert(0, curNode.val);
                    else
                        level.Add(curNode.val);
                    if(curNode.left!=null)
                        queue.Enqueue(curNode.left);
                    if(curNode.right!=null)
                        queue.Enqueue(curNode.right);
                }
                res.Add(level);
                even=!even;
            }
            return res;
        }


        //========================================================================================//
        //----------104. Maximum Depth of Binary Tree--Easy---------------------------------------//
        /*
            Given a binary tree, find its maximum depth.

            The maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.

            Note: A leaf is a node with no children.

            Example:

            Given binary tree [3,9,20,null,null,15,7],

              3
             / \
            9  20
              /  \
             15   7
            return its depth = 3
         */
         /// <summary>
         /// Solution1: DFS with Recursion
         /// Time Complexity: O(n), Space Complexity: O(h) height of the tree for stack space
         /// </summary>
        public int MaxDepthDFSRecursion(TreeNode root) {
            if(root==null)
                return 0;
            int left = MaxDepthDFSRecursion(root.left);
            int right = MaxDepthDFSRecursion(root.right);
            return Math.Max(left,right)+1;
            
        }
        /// <summary>
        /// Solution2: DFS with Iteration
        /// </summary>
        public int MaxDepthDFSIterative(TreeNode root) 
        {
            if(root==null)
                return 0;
            Stack<KeyValuePair<TreeNode, int>> stack = new Stack<KeyValuePair<TreeNode, int>>();
            stack.Push(new KeyValuePair<TreeNode, int>(root,1));
            int max = 0;
            while(stack.Count>0)
            {
                var node = stack.Pop();
                max = Math.Max(node.Value, max);
                if(node.Key.right!=null)
                {
                    stack.Push(new KeyValuePair<TreeNode, int>(node.Key.right, node.Value+1));
                }
                if(node.Key.left!=null)
                {
                    stack.Push(new KeyValuePair<TreeNode, int>(node.Key.left, node.Value+1));
                }
            }
            return max;
        }
        //===============================DFS======================================================//
        //----------108. Convert Sorted Arry to BST--Easy------------------------------------------//
        /*
            Given an array where elements are sorted in ascending order, convert it to a height balanced BST.

            For this problem, a height-balanced binary tree is defined as a binary tree in which the depth of the two subtrees of every node never differ by more than 1.

            Example:

            Given the sorted array: [-10,-3,0,5,9],

            One possible answer is: [0,-3,9,-10,null,5], which represents the following height balanced BST:

                0
               / \
             -3   9
             /   /
            -10  5
        */
        public TreeNode SortedArrayToBST(int[] nums) 
        {
            if(nums==null || nums.Length==0)
                return null;
            return SortedArrayToBSTHelper(nums, 0, nums.Length-1);
        }
        private TreeNode SortedArrayToBSTHelper(int[] nums, int start, int end)
        {
            if(start>end)
                return null;
            int mid = start+(end-start)/2;
            TreeNode root = new TreeNode(nums[mid]);
            root.left = SortedArrayToBSTHelper(nums, start, mid-1);

            root.right = SortedArrayToBSTHelper(nums, mid+1, end);
            return root;
        }

        //===============================DFS======================================================//
        //----------110. Balanced Binary Tree--Easy-----------------------------------------------//
        /*
            Given a binary tree, determine if it is height-balanced.

            For this problem, a height-balanced binary tree is defined as:

            a binary tree in which the depth of the two subtrees of every node never differ by more than 1.

            Example 1:

            Given the following tree [3,9,20,null,null,15,7]:

                3
               / \
              9  20
                /  \
               15   7
            Return true.
        */
        public bool IsBalanced(TreeNode root) 
        {
            int height = TreeHeight(root);
            return height!=-1;
        }
        private int TreeHeight(TreeNode root)
        {
            if(root==null)
            {
                return 0;
            }
            
            int right = TreeHeight(root.right);
            int left = TreeHeight(root.left);
            if (right==-1 || left==-1)
                return -1;
            if(Math.Abs(right-left)>1)
                return -1;
            return Math.Max(right,left)+1;
            
        }


        //===============================DFS======================================================//
        //----------114. Flatten Binary Tree to Linked List--Medium-------------------------------//
        /*
        Given a binary tree, flatten it to a linked list in-place.

            For example, given the following tree:

                1
               / \
              2   5
             / \   \
            3   4   6
            The flattened tree should look like:

            1
             \
              2
               \
                3
                 \
                  4
                   \
                    5
                     \
                      6

         */

        /// <summary>
        /// Recursion
        /// </summary>
        public void FlattenRecursion(TreeNode root) 
        {
            if(root==null)
                return;
            FlattenHelper(root);
        }
        //return the lastNode
        public TreeNode FlattenHelper(TreeNode node)
        {
                
            if(node.left==null && node.right == null)
                return node;
            TreeNode end = node;
            TreeNode endL = null; 
            TreeNode endR = null;
            if(node.left!=null)
            {
                endL = FlattenHelper(node.left);
                end = endL;
            }
            if(node.right!=null)
            {
                endR = FlattenHelper(node.right);
                end = endR;
            }
            
            if(node.left!=null)
            {
                TreeNode temp = node.right;
                node.right = node.left;
                endL.right = temp;
                node.left = null;
            }
            
            return end;
        }
        /// <summary>
        /// DFS use Stack (pre Order root-right-left)
        /// </summary>
        public void Flatten(TreeNode root) 
        {
            if(root==null)
                return;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode dummy = new TreeNode(0);
            TreeNode node = dummy;
            stack.Push(root);
            while(stack.Count>0)
            {
                TreeNode nodePoped = stack.Pop();
                if(nodePoped.right!=null)
                    stack.Push(nodePoped.right);
                if(nodePoped.left !=null)
                    stack.Push(nodePoped.left);
                node.right = nodePoped;
                node = node.right;
                nodePoped.left = null;
            }
            
        }
    }
}