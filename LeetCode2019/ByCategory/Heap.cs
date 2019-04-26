using System;
using System.Collections.Generic;
using LeetCode2019.Shared;

namespace LeetCode2019.ByCategory
{
    public class Heap
    {
        //========================================================================================//
        //----------23.  Merge k Sorted Lists--Hard-----------------------------------------------//
        /*
            Merge k sorted linked lists and return it as one sorted list. Analyze and describe its complexity.

            Example:

            Input:
            [
            1->4->5,
            1->3->4,
            2->6
            ]
            Output: 1->1->2->3->4->4->5->6
        */
        /*---------------Notes--------------------------------------------------------------------- */
        /*Time complexity: O(nlogn) Space Complexity: O(k)
        Use heap since the lists are sorted.
        first push all heads. 
        Heap pop the min then push the next node of this pop result.
         */
        public ListNode MergeKLists(ListNode[] lists) 
        {
            PriorityQueue<ListNode> pq = new PriorityQueue<ListNode>(new ListComparer());
            ListNode dummy = new ListNode(0);
            ListNode node = dummy;
            if(lists==null || lists.Length==0)
                return dummy.next;
            for(int i =0; i<lists.Length; i++)
            {
                if(lists[i]!=null)
                    pq.Push(lists[i]);
            }
            while(pq.Size()>0)
            {
                ListNode cur = pq.Pop();
                node.next = cur;
                if(cur.next!=null)
                    pq.Push(cur.next);
                node = node.next;
            }
            return dummy.next;
        }
        public class ListComparer : IComparer<ListNode> 
        {
            public int Compare(ListNode a, ListNode b) {
                return a.val-b.val;
            }
        }

    }
}