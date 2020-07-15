
using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;
namespace LeetCode2019.ByOrder
{
    public class Top251_300
    {
        //========================================================================================//
        //----------257. Binary Tree Paths--Easy--------------------------------------------------//
        /*
        Given a binary tree, return all root-to-leaf paths.

            Note: A leaf is a node with no children.

            Example:

            Input:

              1
            /   \
            2     3
             \
              5

            Output: ["1->2->5", "1->3"]

            Explanation: All root-to-leaf paths are: 1->2->5, 1->3

         */
        
        public IList<string> BinaryTreePaths(TreeNode root) 
        {
            IList<string> res = new List<string>();
            if(root==null)
                return res;
        
            CombinePath(root.left, res, root.val);
            CombinePath(root.right, res, root.val);
            if(res.Count==0)
            {
                res.Add($"{root.val}");
            }
            return res;
        }
        private void CombinePath(TreeNode node, IList<string> res, int rootVal)
        {
            IList<string> paths = BinaryTreePaths(node);
            if(paths!=null)
            {
                foreach(string path in paths)
                {
                    res.Add($"{rootVal}->{path}");
                }
            }
        }
        //========================================================================================//
        //----------270. Closest Binary Search Tree Value--Easy-----------------------------------//
        /*
            Given a non-empty binary search tree and a target value, find the value in the BST that is closest to the target.

            Note:

            Given target value is a floating point.
            You are guaranteed to have only one unique value in the BST that is closest to the target.
            Example:

            Input: root = [4,2,5,1,3], target = 3.714286

             4
            / \
           2   5
          / \
         1   3

            Output: 4
         */

        //Binary Search Tree, Divide Conquer
        public int ClosestValue(TreeNode root, double target) 
        {
            if(root.left==null && root.right == null)
            {
                return root.val;
            }
            double diff = Math.Abs(target-root.val);
            int resR = root.val, resL = root.val;
            if(root.right!=null && root.val<target)
            {
            resR = ClosestValue(root.right, target);
            return diff>Math.Abs(target-resR)? resR: root.val;
            }
            if(root.left!=null && root.val>target)
            {
                resL = ClosestValue(root.left, target);
                return diff>Math.Abs(target-resL)? resL: root.val;
            }
            return root.val;
            
        }
    }
}