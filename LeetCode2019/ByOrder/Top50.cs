using System;
using System.Collections.Generic;
using LeetCode2019.Shared;

namespace LeetCode2019.ByOrder
{
    public class Top50
    {
        //----------1. Two Sum--Easy----------------------------------//
        /* Given an array of integers, return indices of the two numbers such that they add up to a specific target.
        You may assume that each input would have exactly one solution, and you may not use the same element twice.
        Example:
        Given nums = [2, 7, 11, 15], target = 9,
        Because nums[0] + nums[1] = 2 + 7 = 9,
        return [0, 1].*/

        //-----------Notes----------------------------------------------//
        /*暴力解决： time O(n^2), space O(1) */
        public int[] TwoSumBrute(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length-1; i++)
            {
                for(int j = i+1; j<nums.Length; j++)
                {
                    if (target == nums[i] + nums[j])
                        return new int[] { i, j };
                }
            }
            throw new InvalidOperationException("No result returned");
        }
        /*使用hashmap： time O(n), space O(n)
         * 1. use hashmap to store and look up the elements before current position
         * 2. if there is an element exists add curent nums[i] is target then return the result.
         * 3. if no add current element to hashmap 
         *    (skip if it is already exists in map which means nums[i]+nums[i] is not the target, so duplicate one can should be skipped)*/
        public int[] TwoSumHashMap(int[] nums, int target)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (map.ContainsKey(target - nums[i]))
                {
                    return new int[] { map[target-nums[i]], i};
                }
                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], i);
                }
            }
            throw new InvalidOperationException("No result returned");
        }

        //========================================================================//
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
        //===========================================================================//
        //----------3. Longest Substring Without Repeating Characters--Medium--------//
        /*Given a string, find the length of the longest substring without repeating characters.

            Example 1:

            Input: "abcabcbb"
            Output: 3 
            Explanation: The answer is "abc", with the length of 3. 
            Example 2:

            Input: "bbbbb"
            Output: 1
            Explanation: The answer is "b", with the length of 1.
            Example 3:

            Input: "pwwkew"
            Output: 3
            Explanation: The answer is "wke", with the length of 3. 
        Note that the answer must be a substring, "pwke" is a subsequence and not a substring.*/
        
        //-----------------Notes--------------------------------------------------//
        /* Time: O(n) Space: O(n) 
           Sliding window, and track visited characters in hash map.
           Remember the start position and end position, 
           if the char in end position exists in hashmap and the index is greater or equal to the start position which means there are duplicates.
           so slid the window to change the start position to the right of the first appreance of the duplicate char*/
        public int LengthOfLongestSubstring(string s) 
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }
            Dictionary<char, int> map = new Dictionary<char, int>();
            int start = 0, end = 0;
            int res = 0;
            while(end<s.Length)
            {
                if(map.ContainsKey(s[end]))
                {
                    if(map[s[end]]>=start)
                    {
                        start = map[s[end]]+1;
                    }
                    map[s[end]]=end;
                }
                else
                {
                    map.Add(s[end], end);
                }

                res= Math.Max(res, end-start+1);
                end++;
            }
            return res;
        }
        //============================================================================//
        //----------4. Median of Two Sorted Arrays--Hard------------------------------//
        /*There are two sorted arrays nums1 and nums2 of size m and n respectively.
            Find the median of the two sorted arrays. The overall run time complexity should be O(log (m+n)).
            You may assume nums1 and nums2 cannot be both empty.
            Example 1:
            nums1 = [1, 3]
            nums2 = [2]
            The median is 2.0
            Example 2:
            nums1 = [1, 2]
            nums2 = [3, 4]
            The median is (2 + 3)/2 = 2.5
        */
        //-----------------Notes--------------------------------------------------//
        /*这个题的思路是中点分奇数和偶数两种情况。找到在中间位置的两个或者一个数即可。
            可以认为是找第K大的值
            利用二分法，每次找在两个数组中找到第k/2大的元素相比较， 
            更新起始位置到相对较小的一个数组。就是说这个数组的前k/2 个数可以被扔掉。在剩下的元素中找第k-k/2大的数*/ 
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int a = nums1.Length, b = nums2.Length;
            //Note: 当两个数组长度之和是偶数的时候，需要考虑中间两个点的和的一半为Median
            if((a+b)%2==0)
                return (FindKthElement(nums1, 0, nums2, 0, (a+b)/2)+FindKthElement(nums1,0,nums2,0, (a+b)/2+1))/2.0;
            else
                return FindKthElement(nums1,0,nums2,0,(a+b)/2+1);
        }
        private int FindKthElement(int[] nums1, int start1, int[] nums2, int start2, int k)
        {
            //Note: 当起始点的index大于数组长度则证明结果肯定在另外的数组的第k个值
            if(start1>=nums1.Length)
                return nums2[start2+k-1];
            if(start2>=nums2.Length)
                return nums1[start1+k-1];
            //Note: Recursion 的重点是什么时候跳出递归
            if(k==1)
                return Math.Min(nums1[start1], nums2[start2]);
            int mid1 = start1+k/2-1;
            int mid2 = start2+k/2-1;
            //Note: 当第k/2的index overflow了数组的长度则把值代替成最大值这样可以忽略这种overflow的情况因为另外一个相比较的值永远小，
            //      所以只会更新另一半继续进行递归。
            int midVal1 = mid1>=nums1.Length? Int32.MaxValue:nums1[mid1];
            int midVal2 = mid2>=nums2.Length? Int32.MaxValue:nums2[mid2];
            //Note: 需要在mid基础上+1 因为下一个循环需要从下一位开始。
            if(midVal1<midVal2)
            {
                return FindKthElement(nums1,mid1+1,nums2, start2, k-k/2);
            }
            return FindKthElement(nums1, start1, nums2, mid2+1, k-k/2);
        }
    }
}
