using System;
using System.Collections.Generic;
using LeetCode2019.Shared;

namespace LeetCode2019.ByCategory
{
    public class LinkedList
    {
        //==============================================================================//
        //----------2. Add Two Numbers--Medium------------------------------------------//
        /*You are given two non-empty linked lists representing two non-negative integers. The digits are stored in reverse order and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.
            You may assume the two numbers do not contain any leading zero, except the number 0 itself.

        Example:

        Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
        Output: 7 -> 0 -> 8
        Explanation: 342 + 465 = 807.*/
        
        //-----------------Notes--------------------------------------------------//
        /* Time: O(n) Space: O(n) say n is longer length of L1 and l2
           这道题主要注意优化代码， 为了避免冗余代码需要check如果node为null的情况。
           运算结束需要考虑carry 是不是1 如果是则还需一位n*/
        public ListNode AddTwoNumber(ListNode l1, ListNode l2)
        {
            ListNode node1 = l1;
            ListNode node2 = l2;
            ListNode dummy = new ListNode(0);
            ListNode res = dummy;
            int carry = 0;
            while(node1!=null || node2!=null)
            {
                int a = (node1==null)?0:node1.val;
                int b = (node2==null)?0:node2.val;
                res.next = new ListNode((a+b+carry)%10);
                carry = (a+b+carry)/10;
                node1 = (node1==null)?null: node1.next;
                node2 = (node2==null)?null: node2.next;
                res = res.next;
            }
            if(carry==1)
            {
                res.next = new ListNode(1);
            }
            return dummy.next;
        }
        //========================================================================================//
        //----------19. Remove Nth Node From End of List--Medium----------------------------------//
        /*
            Given a linked list, remove the n-th node from the end of list and return its head.

            Example:

            Given linked list: 1->2->3->4->5, and n = 2.

            After removing the second node from the end, the linked list becomes 1->2->3->5.
            Note:

            Given n will always be valid.

            Follow up:

            Could you do this in one pass?
         */
         /* not one pass,  1. find the length of the list, 2. delete the node*/
        public ListNode RemoveNthFromEnd(ListNode head, int n) 
        {
            int length = CountLength(head);
            int target = length-n;
            ListNode node = head;
            if(target==0)
            {
                return head.next;
            }
            while(target>1)
            {
                node = node.next;
                target--;
            }
            node.next = node.next.next;
            return head;
        }
        private int CountLength(ListNode head)
        {
            ListNode node = head;
            int res = 0;
            while(node!=null)
            {
                res++;
                node = node.next;
            }
            return res;
        }

        /* One pass solution: two pointer to have one fast pointer to be ahead of slow pointer with n steps. 
        then when fastpointer pointing to null we can delete next node of slow pointer. 
        need to handle when delete the head node, or we can have a dummy head.
        */
        public ListNode RemoveNthFromEndOnePass(ListNode head, int n) 
        {
            ListNode slow = head;
            ListNode fast = head;
            while(n>0)
            {
                fast=fast.next;
                n--;
            }
            if(fast==null)
                return head.next;
            while(fast.next!=null)
            {
                slow=slow.next;
                fast= fast.next;
            }
            slow.next= slow.next.next;
            return head;
        }
        //========================================================================================//
        //----------24.  Swap Nodes in Pairs--Medium----------------------------------------------//
        /*
        Given a linked list, swap every two adjacent nodes and return its head.

        Example:

        Given 1->2->3->4, you should return the list as 2->1->4->3.
        Note:

        Your algorithm should use only constant extra space.
        You may not modify the values in the list's nodes, only nodes itself may be changed.
         */
        public ListNode SwapPairs(ListNode head) 
        {
            ListNode dummy = new ListNode(0);
            dummy.next = head;
            ListNode prev = dummy;
            ListNode node = head;
            while(node!=null && node.next!=null)
            {
                ListNode temp = node.next.next;
                prev.next = node.next;
                node.next = temp;
                prev.next.next = node;
                prev = node;
                node = node.next;
            }
            return dummy.next;
        }
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
    }
}