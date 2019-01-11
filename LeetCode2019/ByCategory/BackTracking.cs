using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;

namespace LeetCode2019.ByCategory
{
    public class BackTracking
    {
        //========================================================================================//
        //----------17. Letter Combinations of a Phone Number--Medium-----------------------------//
        /*
        Given a string containing digits from 2-9 inclusive, return all possible letter combinations that the number could represent.

        A mapping of digit to letters (just like on the telephone buttons) is given below. Note that 1 does not map to any letters.

        Example:

        Input: "23"
        Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
        */
        /*--------------------------Notes-------------------------------------------------------- */
        /*
            相当于组合出所有可能。如果n 为input的长度则最多可能输出 4^n 个结果。并且每一次找到一个结果都要recursion n次
            所以Time Complexity 为 O(n*(4^n)); space complexity : O(n*(4^n));

            backtracking
            当pos为input长度的时候退出recursion 输出当前结果。
            当返回上一层的时候把当前string里最后一位remove。
         */
        public IList<string> LetterCombinations(string digits) 
        {
            IList<string> result = new List<string>();
            if(string.IsNullOrEmpty(digits))
                return result;
            Dictionary<char,List<char>> map = new Dictionary<char,List<char>>();
            map.Add('2', new List<char>{'a','b','c'});
            map.Add('3', new List<char>{'d','e','f'});
            map.Add('4', new List<char>{'g','h','i'});
            map.Add('5', new List<char>{'j','k','l'});
            map.Add('6', new List<char>{'m','n','o'});
            map.Add('7', new List<char>{'p','q','r','s'});
            map.Add('8', new List<char>{'t','u','v'});
            map.Add('9', new List<char>{'w','x','y','z'});
            
            StringBuilder sb = new StringBuilder();
            char[] arr = digits.ToCharArray();
            Helper(arr, map, 0, result, sb);
            return result;
            
        }
        private void Helper(char[] arr, Dictionary<char, List<char>> map, int pos, IList<string> res, StringBuilder sb)
        {
            if(pos==arr.Length)
            {
                res.Add(sb.ToString());
            }
            else
            {
                List<char> chars = map[arr[pos]];
                for(int i = 0; i<chars.Count; i++)
                {
                    sb.Append(chars[i]);
                    Helper(arr, map, pos+1, res, sb);
                    sb.Remove(sb.Length-1, 1);
                }
            }
        }
    
    }
}