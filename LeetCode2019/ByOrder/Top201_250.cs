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
    }
}