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
    }
}