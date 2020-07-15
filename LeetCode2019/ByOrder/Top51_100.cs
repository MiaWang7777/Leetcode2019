using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;

namespace LeetCode2019.ByOrder
{
    public class Top51_100
    {
        //========================================================================================//
        //----------53. Maximum Subarray--Easy----------------------------------------------------//
        /*
        Given an integer array nums, find the contiguous subarray (containing at least one number) which has the largest sum and return its sum.

            Example:

            Input: [-2,1,-3,4,-1,2,1,-5,4],
            Output: 6
            Explanation: [4,-1,2,1] has the largest sum = 6.
            Follow up:

            If you have figured out the O(n) solution, try coding another solution using the divide and conquer approach, which is more subtle.
        */
        /// <summary>
        /// Divide and conqure -> sliding window problem 
        /// when the cur sum is less then the cur index value then we can start a new sum.
        ///  [-2], 1,  -3,  4,  -1,  2,  1,  -5,  4
        ///  -2,  [1] ,-3,  4,  -1,  2,  1,  -5,  4
        ///  -2,  [1 , -3], 4,  -1,  2,  1,  -5,  4
        ///  -2,   1 , -3, [4], -1,  2,  1,  -5,  4
        ///  -2,   1 ,-3,  [4,  -1], 2,  1,  -5,  4
        ///  -2,   1 ,-3,  [4,  -1,  2], 1,  -5,  4
        ///  -2,   1 ,-3,  [4,  -1,  2,  1], -5,  4
        ///  -2,   1 ,-3,  [4,  -1,  2,  1,  -5], 4
        ///  -2,   1 ,-3,   4,  -1,  2,  1,  -5, [4]
        /// </summary>
        public int MaxSubArray(int[] nums) 
        {
            int max = nums[0];
            int sum = nums[0];
            for(int i =1; i<nums.Length; i++)
            {
                if(nums[i]+sum >nums[i])
                {
                    sum+=nums[i];
                    max = Math.Max(sum, max);
                }
                else
                {
                    sum = nums[i];
                    max = Math.Max(sum, max);
                }
            }
            return max;
        }
        //========================================================================================//
        //----------92.  Reverse Linked List II--Medium------------------------------------------//
        /*
        Reverse a linked list from position m to n. Do it in one-pass.

        Note: 1 ≤ m ≤ n ≤ length of list.

        Example:

        Input: 1->2->3->4->5->NULL, m = 2, n = 4
        Output: 1->4->3->2->5->NULL
         */
        public ListNode ReverseBetween(ListNode head, int m, int n) {
            ListNode dummy = new ListNode(0);
            dummy.next = head;
            ListNode node = head;
            ListNode prev = dummy;
            ListNode reversedHead = null;
            ListNode reversedTail = null;
            int count =0;
            while(node!=null)
            {
                count++;
                ListNode temp = node.next;
                if(count==m-1)
                {
                    prev = node;
                }
                else if(count>=m && count<=n)
                {
                    
                    node.next = reversedHead;
                    reversedHead = node;
                    
                    if(count==m)
                    {
                        reversedTail = reversedHead;
                    }
                    if(count==n)
                    {
                        prev.next = reversedHead;
                        reversedTail.next =temp;
                    }
                }
                node = temp;
            }
            return dummy.next;
        }
        
        //========================================================================================//
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
    }
}