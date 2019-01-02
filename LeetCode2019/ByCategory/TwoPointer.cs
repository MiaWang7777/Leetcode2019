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
    }
}