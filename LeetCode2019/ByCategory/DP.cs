using System;
using System.Collections.Generic;
using LeetCode2019.Shared;

namespace LeetCode2019.ByCategory
{
    public class DP
    {
        //========================================================================================//
        //----------32. Longest Valid Parentheses--Hard-------------------------------------------// 
        /*
            Given a string containing just the characters '(' and ')', find the length of the longest valid (well-formed) parentheses substring.

            Example 1:

            Input: "(()"
            Output: 2
            Explanation: The longest valid parentheses substring is "()"
            Example 2:

            Input: ")()())"
            Output: 4
            Explanation: The longest valid parentheses substring is "()()"
         */
        public int LongestValidParentheses(string s) 
        {
            if(string.IsNullOrEmpty(s))
                return 0;
            int[] dp = new int[s.Length];
            int max = 0;
            dp[0]=0;
            for(int i =1; i<s.Length; i++)
            {
                if(s[i]==')')
                {
                    if(s[i-1]=='(')
                    {
                        dp[i]= i-2>=0?dp[i-2]+2:2;
                    }
                    else if(i-dp[i-1]-1>=0 )
                    {
                        if(s[i-dp[i-1]-1]=='(')
                        {
                            dp[i]=dp[i-1]+2;
                            if(i-dp[i]>=0)
                            {
                                dp[i]+=dp[i-dp[i]];
                            }
                            
                        }
                    }
                    max = Math.Max(dp[i], max);
                }
            }
            return max;
        }
    }
}