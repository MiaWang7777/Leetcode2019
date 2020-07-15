using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;
namespace LeetCode2019.ByOrder
{
    public class Top201_250
    {
        //========================================================================================//
        //----------206.  Reverse Linked List--Easy----------------------------------------------//
        /*
        Reverse a singly linked list
         */
         
         // Iteration: Time O(n), Space O(1)
         public ListNode ReverseList(ListNode head)
         {
             //cur
             ListNode node = head;
             //prev
             ListNode reverved = null;
             while(node!=null)
             {
                 //next
                 ListNode temp = node.next;
                 node.next = reverved;
                 reverved = node;
                 node = temp;
             }
             return reverved;
         }
         //Recursion: Time O(b), Space O(n)(stack space)
         public ListNode ReverseListRecursion(ListNode head)
         {
            if(head == null || head.next == null)
                return head;
            ListNode reversedHead = ReverseListRecursion(head.next);
            //1->2->3->4<-5 
            //      |     |
            //     head   reversedHead   
            // current step need to make 
            //1->2->3<-4<-5
            head.next.next = head;
            head.next = null;
            return reversedHead;
         }

        //========================================================================================//
        //----------230.  Kth Smallest Element in a BST--Medium------------------------------------//
        /*
        Given a binary search tree, write a function kthSmallest to find the kth smallest element in it.

        Note: 
        You may assume k is always valid, 1 ≤ k ≤ BST's total elements.

        Example 1:

        Input: root = [3,1,4,null,2], k = 1
          3
         / \
        1   4
         \
          2
        Output: 1
        Example 2:

        Input: root = [5,3,6,2,4,null,null,1], k = 3
             5
            / \
           3   6
          / \
         2   4
         /
        1
        Output: 3

         */
        public int KthSmallest(TreeNode root, int k) 
        {
            int[] res = Helper(root, k);
            return res[1];
        }
        private int[] Helper(TreeNode node, int k)
        {
            if(node == null)
                return new int[]{0,0};
            int[] left = Helper(node.left, k);
            if(left[0]>=k)
            {
                return left;
            }
            left[0]++;
            if(left[0]==k)
            {
                left[1] = node.val;
                return left;
            }
            int[] right = Helper(node.right, k-left[0]);
            right[0]+=left[0];
            return right;
            
        }
    }
}