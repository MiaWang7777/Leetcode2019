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
    }
}