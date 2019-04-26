using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;

namespace LeetCode2019.ByCategory
{
    public class StackAndQueue
    {
        //========================================================================================//
        //----------20.  Valid Parentheses--Easy--------------------------------------------------//
        /*
        Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

        An input string is valid if:

        Open brackets must be closed by the same type of brackets.
        Open brackets must be closed in the correct order.
        Note that an empty string is also considered valid.
        Example 2:

        Input: "()[]{}"
        Output: true
        Example 3:

        Input: "(]"
        Output: false
         */

        public bool IsValid(string s) 
        {
            if(string.IsNullOrEmpty(s))
                return true;
            Stack<char> stack = new Stack<char>();
            for(int i =0; i<s.Length; i++)
            {
                if(s[i]=='('||s[i]=='{'||s[i]=='[')
                {
                    stack.Push(s[i]);
                }
                else
                {
                    if(stack.Count!=0)
                    {
                        char cur = stack.Pop();
                        if(s[i]==')'&& cur!='('||s[i]=='}'&& cur!='{'||s[i]==']' && cur!= '[')
                            return false;
                    }
                    else
                        return false;
                }
            }
            if(stack.Count!=0)
                return false;
            return true;
        }

    }
}