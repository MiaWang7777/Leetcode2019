using System;
using System.Collections.Generic;
using LeetCode2019.Shared;

namespace LeetCode2019.ByCategory
{
    public class TwoPointer
    {
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
                //========================================================================================//
        //----------15. 3 Sum--Medium-------------------------------------------------------------//
        /*
        Given an array nums of n integers, are there elements a, b, c in nums such that a + b + c = 0? Find all unique triplets in the array which gives the sum of zero.

        Note:

        The solution set must not contain duplicate triplets.

        Example:

        Given array nums = [-1, 0, 1, 2, -1, -4],

        A solution set is:
        [
        [-1, 0, 1],
        [-1, -1, 2]
        ]
         */
        
        /*---------------------Notes----------------------------------------------------------- */
        /*
        Two pointer
        Time complexity: O(n^2+nlogn) 则为 O（n^2）; space complexity O(1);
        这个题算是two sum 变形，为了避免重复则需要排序以便跳过重复的数字。
        过一遍数组 在当前index 之后的所有数里找所有可能的两个数与当前数之和为0的结果， 这一步则把问题变成了找two sum
        需要注意的是当碰到当前的数 与前一个index的数相同时则需要跳过 以避免重复的结果。
         */

        public IList<IList<int>> ThreeSum(int[] nums) 
        {
            Array.Sort(nums);
            IList<IList<int>> res = new List<IList<int>>();
            for(int i =0; i<nums.Length; i++)
            {
                //if duplicated element, then skip
                if(i>0 && nums[i]==nums[i-1])
                    continue;
                //Find all twosum results
                int p1=i+1, p2 = nums.Length-1;
                while(p1<p2)
                {
                    //skip duplicated element
                    if(p1>i+1 && nums[p1]==nums[p1-1])
                    {
                        p1++;
                        continue;
                    }
                    if(p2<nums.Length-1 && nums[p2]==nums[p2+1])
                    {
                        p2--;
                        continue;
                    }

                    //if result, add to the res.
                    //if greater, move p2
                    //if smaller, move p1
                    if(nums[p1]+nums[p2]==-1*nums[i])
                    {
                        res.Add(new List<int>{nums[i], nums[p1], nums[p2]});
                        p1++; p2--;
                    }
                    else if(nums[p1]+nums[p2]>-1*nums[i])
                    {
                        p2--;
                    }
                    else
                        p1++;
                }
            }
            return res;
        }
        
        //========================================================================================//
        //----------16. 3 Sum Closest--Medium-------------------------------------------------------------//
        /*
        Given an array nums of n integers and an integer target, find three integers in nums such that the sum is closest to target. Return the sum of the three integers. You may assume that each input would have exactly one solution.

        Example:

        Given array nums = [-1, 2, 1, -4], and target = 1.

        The sum that is closest to the target is 2. (-1 + 2 + 1 = 2).
         */
        public int ThreeSumClosest(int[] nums, int target)
        {
            int closest = nums[0]+nums[1]+nums[2];
            Array.Sort(nums);
            for(int i =0; i<nums.Length; i++)
            {
                int p1=i+1; int p2 = nums.Length-1;
                while(p1<p2)
                {
                    int sum = nums[i]+nums[p1]+nums[p2];
                    closest = Math.Abs(closest-target)>Math.Abs(sum-target)?sum:closest;
                    if(nums[p1]+nums[p2]>target-nums[i])
                    {
                    p2--;
                    }
                    else
                    {
                        p1++;
                    }
                }
            }
            return closest;
        }

        //========================================================================================//
        //----------18. 4 Sum--Medium-------------------------------------------------------------//
        /*
            Given an array nums of n integers and an integer target, are there elements a, b, c, and d in nums such that a + b + c + d = target? Find all unique quadruplets in the array which gives the sum of target.

            Note:

            The solution set must not contain duplicate quadruplets.

            Example:

            Given array nums = [1, 0, -1, 0, -2, 2], and target = 0.

            A solution set is:
            [
            [-1,  0, 0, 1],
            [-2, -1, 1, 2],
            [-2,  0, 0, 2]
            ]
         */
         /*----------------------Notes---------------------------------------------------------- */
         /* 4 sum -> 3 sum -> 2 sum
            Time complexity: O(n^3) Space Complexity: O(1)
          */
        public IList<IList<int>> FourSum(int[] nums, int target) 
        {
            IList<IList<int>> res = new List<IList<int>>();
            if(nums==null || nums.Length ==0)
                return res;
            Array.Sort(nums);
            for(int i =0; i<nums.Length; i++)
            {
                if(i>0 && nums[i]==nums[i-1])
                    continue;
                for(int j =i+1; j<nums.Length; j++)
                {
                    if(j>i+1 && nums[j]==nums[j-1])
                        continue;
                    int p1=j+1; int p2=nums.Length-1;
                    while(p1<p2)
                    {
                        if(p1>j+1 && nums[p1]==nums[p1-1])
                        {
                            p1++;
                            continue;
                        }

                        if(p2<nums.Length-1 && nums[p2]==nums[p2+1])
                        {
                            p2--;
                            continue;
                        }
                        if(nums[p1]+nums[p2]+nums[i]+nums[j]==target)
                        {
                            res.Add(new List<int>{nums[i], nums[j], nums[p1], nums[p2]});
                            p1++;
                            p2--;
                        }
                        else if(nums[p1]+nums[p2]+nums[i]+nums[j]>target)
                        {
                            p2--;
                        }
                        else
                            p1++;
                    }
                }
            }
            return res;
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
        //----------26.  Remove Duplicates from Sorted Array--Easy--------------------------------//
        /*
        Given a sorted array nums, remove the duplicates in-place such that each element appear only once and return the new length.

        Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.

        Example 1:

        Given nums = [1,1,2],

        Your function should return length = 2, with the first two elements of nums being 1 and 2 respectively.

        It doesn't matter what you leave beyond the returned length.
        Example 2:

        Given nums = [0,0,1,1,1,2,2,3,3,4],

        Your function should return length = 5, with the first five elements of nums being modified to 0, 1, 2, 3, and 4 respectively.

        It doesn't matter what values are set beyond the returned length.
         */
        /*-------------------------Notes------------------------------------------------------- */
        /*
        Two pointer p1 pointing to the pos that need to be swapped, p2 is looping through for the next value
         */
        public int RemoveDuplicates(int[] nums) 
        {
            if(nums==null || nums.Length==0)
                return 0;

        //herer can be refactored, if p1==p2 just ignore
            int p1=0;
            while(p1<nums.Length && nums[p1]!=nums[p1-1])
            {
                p1++;
            }
            int p2= p1;
            while(p2<nums.Length)
            {
                if(nums[p2]>nums[p1-1])
                {
                    nums[p1] = nums[p2];
                    p1++;
                }
                p2++;
            }
            return p1;
        }
        public int RemoveDuplicatesRefactored(int[] nums) 
        {
            if(nums==null || nums.Length==0)
                return 0;
            int p1 =0;
            for(int i=1; i<nums.Length; i++)
            {
                if(nums[i]!=nums[p1])
                {
                    p1++;
                    nums[p1] = nums[i];
                }
            }
            return p1+1;
        }

                //----------27. Remove Element--Easy--------------------------------//
        /*
        Given an array nums and a value val, remove all instances of that value in-place and return the new length.
        Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.
        The order of elements can be changed. It doesn't matter what you leave beyond the new length.
        Example 1:
        Given nums = [3,2,2,3], val = 3,
        Your function should return length = 2, with the first two elements of nums being 2.
        It doesn't matter what you leave beyond the returned length.
        Example 2:
        Given nums = [0,1,2,2,3,0,4,2], val = 2,
        Your function should return length = 5, with the first five elements of nums containing 0, 1, 3, 0, and 4.
        Note that the order of those five elements can be arbitrary.
        It doesn't matter what values are set beyond the returned length.
        Clarification:
        Confused why the returned value is an integer but your answer is an array?
        Note that the input array is passed in by reference, which means modification to the input array will be known to the caller as well.
        Internally you can think of this:
        // nums is passed in by reference. (i.e., without making a copy)
        int len = removeElement(nums, val);
        // any modification to nums in your function would be known by the caller.
        // using the length returned by your function, it prints the first len elements.
        for (int i = 0; i < len; i++) {
            print(nums[i]);
        }
         */
        public int RemoveElement(int[] nums, int val) 
        {
            if(nums==null|| nums.Length==0)
                return 0;
            int p1 =0;
            int p2 = nums.Length-1;
            while(p1<=p2)
            {
                if(nums[p1]==val)
                {
                    nums[p1]=nums[p2];
                    p2--;
                }
                else
                    p1++;
            }
            return p2+1;
        }
        //========================================================================================//
        //----------28. Implement strStr()--Easy--------------------------------------------------// 
        /*
            Implement strStr().

            Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.

            Example 1:

            Input: haystack = "hello", needle = "ll"
            Output: 2
            Example 2:

            Input: haystack = "aaaaa", needle = "bba"
            Output: -1
        */      
        public int StrStr(string haystack, string needle) 
        {
            if(string.IsNullOrEmpty(needle) )
                return 0;
            for(int i = 0; i<haystack.Length;i++)
            {
                for(int j=0; j<needle.Length; j++)
                {
                    if(i+j==haystack.Length)
                        return -1;
                    if(haystack[i+j]!=needle[j])
                        break;
                    else if(j==needle.Length-1)
                    {
                        return i;
                    }
                }
            }
            return -1;
        } 
    }
}