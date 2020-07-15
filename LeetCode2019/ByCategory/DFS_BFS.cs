using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;

namespace LeetCode2019.ByCategory
{
    public class DFS_BFS
    {
        //=======================================DFS=================================================//
        //----------104. Maximum Depth of Binary Tree--Easy-----------------------------------------//
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
        public int MaxDepthDFSIterative(TreeNode root) {
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
        //===============================DFS/BFS==================================================//
        //----------200. Number of Islands--Medium------------------------------------------------//
        /*
            Given a 2d grid map of '1's (land) and '0's (water), count the number of islands. An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. You may assume all four edges of the grid are all surrounded by water.

            Example 1:

            Input:
            11110
            11010
            11000
            00000

            Output: 1
         */
         /// <summary>
         /// BFS
         /// Time Complexity: O(mn), Space Complexity: O(min[m,n])
         /// </summary>
        public int NumIslandsBFS(char[][] grid) 
        {
            if(grid.Length==0|| grid[0].Length==0)
                return 0;
            Queue<int[]> queue = new Queue<int[]>();
            int count = 0;
            int m = grid.Length;
            int n = grid[0].Length;
            int[,] direction = new int[,]{{1,0,-1,0},{0,1,0,-1}};
            for(int i =0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if(grid[i][j]=='1')
                    {
                        count++;
                        queue.Enqueue(new int[]{i,j});
                        grid[i][j] = '0';
                        while(queue.Count>0)
                        {
                            int[] cur = queue.Dequeue();
                            
                            for(int k=0; k<4; k++)
                            {
                                int a = cur[0]+direction[0,k];
                                int b = cur[1]+direction[1,k];
                                if(InBound(a, b,m,n) && grid[a][b]=='1')
                                {
                                    queue.Enqueue(new int[]{a,b});
                                    grid[a][b] = '0';
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }
        /// <summary>
        /// DFS Iterative
        /// Time Complexity: O(mn), Space Complexity: O(mn)
        /// </summary>
        public int NumIslandsDFSIterative(char[][] grid) 
        {
            if(grid.Length==0|| grid[0].Length==0)
                return 0;
            Stack<int[]> stack = new Stack<int[]>();
            int count = 0;
            int m = grid.Length;
            int n = grid[0].Length;
            int[,] direction = new int[,]{{1,0,-1,0},{0,1,0,-1}};
            for(int i =0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if(grid[i][j]=='1')
                    {
                        count++;
                        stack.Push(new int[]{i,j});
                        grid[i][j] = '0';
                        while(stack.Count>0)
                        {
                            int[] cur = stack.Pop();
                            
                            for(int k=0; k<4; k++)
                            {
                                int a = cur[0]+direction[0,k];
                                int b = cur[1]+direction[1,k];
                                if(InBound(a, b,m,n) && grid[a][b]=='1')
                                {
                                    stack.Push(new int[]{a,b});
                                    grid[a][b] = '0';
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// DFS Recursion 
        /// Time Complexity: O(mn), Space Complexity: O(mn)
        /// </summary>
        public int NumIslandsDFS(char[][] grid) 
        {
            if(grid.Length==0|| grid[0].Length==0)
                return 0;
            int count = 0;
            int m = grid.Length;
            int n = grid[0].Length;
        
            for(int i =0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if(grid[i][j]=='1')
                    {
                        count++;
                        Dfs(grid,i,j);
                    }
                }
            }
            return count;
        }

        private void Dfs(char[][] grid, int a, int b)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            int[,] direction = new int[,]{{1,0,-1,0},{0,1,0,-1}};
            
            //End condition of recursion
            if(!InBound(a, b,m,n) || grid[a][b]=='0')
                return;
        
            grid[a][b] = '0';
            for(int k=0; k<4; k++)
            {
                int c = a+direction[0,k];
                int d = b+direction[1,k];
                Dfs(grid, c, d);
            }
            
        }
        private bool InBound(int a, int b, int m, int n)
        {
            if(a>=0&& a<m && b>=0&& b<n)
                return true;
            return false;
        }

        //========================DFS=============================================================//
        //----------100.  Same Tree--Easy---------------------------------------------------------//
        /*
        Given two binary trees, write a function to check if they are the same or not.

        Two binary trees are considered the same if they are structurally identical and the nodes have the same value.

        Example 1:

        Input:     1         1
                  / \       / \
                 2   3     2   3

                [1,2,3],   [1,2,3]

        Output: true
         */
         /// <summary>
         /// Recursion DFS
         /// Time: O(n) N is number of the tree, Space: O(log(n)) stack space for Balanced trees. O(n) for worst case
         /// </summary>

        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if(p==null&&q==null)
                return true;
            if(p==null || q==null)
                return false;
            if(p.val==q.val)
            {
                return IsSameTree(p.right, q.right) && IsSameTree(p.left, q.left);
            }
            return false;
        }
        /// <summary>
        /// Iterative
        /// </summary>
        public bool IsSameTreeIterative(TreeNode p, TreeNode q) 
        {
            if(p==null && q==null)
                return true;
            if(!CompareNodes(p, q))
                    return false;
            Stack<TreeNode> pStack = new Stack<TreeNode>();
            Stack<TreeNode> qStack = new Stack<TreeNode>();
            pStack.Push(p);
            qStack.Push(q);
            while(pStack.Count>0&& qStack.Count>0)
            {
                TreeNode nodeP = pStack.Pop();
                TreeNode nodeQ = qStack.Pop();
                if(!CompareNodes(nodeP, nodeQ))
                    return false;

                if(nodeP.left!=null && nodeQ.left!=null)
                {
                    pStack.Push(nodeP.left);
                    qStack.Push(nodeQ.left);
                }
                if(!CompareNodes(nodeP.left, nodeQ.left))
                    return false;
                if(!CompareNodes(nodeP.right, nodeQ.right))
                    return false;
                if(nodeP.right!=null && nodeQ.right!=null)
                {
                    pStack.Push(nodeP.right);
                    qStack.Push(nodeQ.right);
                }
            }
            
            if(pStack.Count==0 && qStack.Count==0)
                return true;
            return false;
        }
        private bool CompareNodes(TreeNode p, TreeNode q)
        {
            if (p == null && q == null) return true;
            if (q == null || p == null) return false;
            if (p.val != q.val) return false;
            return true;
        }

        //==========================DFS/BFS====================================================//
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
    }
}