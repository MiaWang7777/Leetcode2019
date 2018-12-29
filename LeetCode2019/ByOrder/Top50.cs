using System;
using System.Collections.Generic;
using LeetCode2019.Shared;

namespace LeetCode2019.ByOrder
{
    public class Top50
    {
        //----------1. Two Sum------------------------------------//
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
        //----------2. Add Two Numbers--------------------------------------------//

    }
}
