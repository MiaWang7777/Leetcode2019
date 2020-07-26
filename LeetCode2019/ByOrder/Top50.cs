using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;

namespace LeetCode2019.ByOrder
{
    /*
    Need To Review！
    1. Two Sum--Easy
    3. Longest Substring Without Repeating Characters--Medium
    4. Median of Two Sorted Arrays--Hard
    5. Longest Palindromic Substring--Medium
    10. Regular Expression Matching--Hard
    15. 3 Sum--Medium
    22.  Generate Parentheses--Medium
    31. Next Permutation--Medium
    32. Longest Valid Parentheses--Hard
     */
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
        /*使用 two pass hashmap*/
        public int[] TwoSumTwoPassHashMap(int[] nums, int target) {
            IDictionary<int, int> map = new Dictionary<int, int>();
            for(int i = 0; i<nums.Length; i++)
            {
                if(!map.ContainsKey(nums[i]))
                    map.Add(nums[i], i);
                else
                {
                    map[nums[i]] = i;
                }
            }
            for(int i = 0; i<nums.Length; i++)
            {
                int remain = target-nums[i];
                if(map.ContainsKey(remain) && map[remain]!=i)
                    return new int[]{i, map[remain]};
            }
            throw new ArgumentException("No result found");
         }
        /*使用 one pass hashmap： time O(n), space O(n)
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

        //================================================================================//
        //----------5. Longest Palindromic Substring--Medium------------------------------//
        /*
            Given a string s, find the longest palindromic substring in s. You may assume that the maximum length of s is 1000.

            Example 1:

            Input: "babad"
            Output: "bab"
            Note: "aba" is also a valid answer.
            Example 2:

            Input: "cbbd"
            Output: "bb"
        */ 
        //-----------------Notes--------------------------------------------------//
        /* 
            Solution 1 暴力解决: 找出以每一个点为起始的最长回文
                        Time complexity: O(n^3), space complexity:O(1)
        */
        public string LongestPalindromeBrute(string s) {
            if(string.IsNullOrEmpty(s))
                return "";
            int start =0, end = 0;
            for(int i = 0; i<s.Length; i++)
            {
                for(int j=i; j<s.Length; j++)
                {
                    if(IsPalindrom(s,i,j) && j-i>end-start)
                    {
                        start = i; end = j;
                    }
                }
            }
            return s.Substring(start, end-start+1);
        }
        private bool IsPalindrom(string s, int start, int end)
        {
            while(start<end && s[start]==s[end])
            {
                start++;
                end--;
            }
            if(start<end)
                return false;
            return true;
        }
        /* 
            Solution 2: Dynamic Programming 由暴力解决办法简化而来。记住由i到j的substring是不是回文从而可以不需要重复检查
                        substring(i,j) --> 回文 if(s[i]==s[j] && substring(i+1, j-1) 是回文)
                        Time complexity: O(n^2), space complexity:O(n^2)

                        需要注意的是提前fill bool[,] 当i==j和当i=j-1的时候为true 代表substring长度为1 和2的时候与其他substring无关
                        当计算当前位置是否为回文的时候需要注意循环的写法是一层一层fill的
                        11          11          11
                        011         111         111
                        0011        0011        1111
                        00011       00011       00011
                        初始状态
        */
        public string LongestPalindromeDP(string s) {
            if(string.IsNullOrEmpty(s))
                return "";
            int start =0, end = 0;
            bool[,] isPalindrom = new bool[s.Length,s.Length];
            for(int i =0; i<s.Length;i++)
            {
                isPalindrom[i,i] = true;
                if(i<s.Length-1)
                    isPalindrom[i+1, i] = true;
            }
            for(int j = 1; j<s.Length; j++)
            {
                for(int i=0; i<j; i++)
                {
                    isPalindrom[i,j] = (s[i]==s[j])&&isPalindrom[i+1, j-1];
                    if(isPalindrom[i,j] && j-i>end-start)
                    {
                        start=i; end = j;
                    }
                }
            }
            return s.Substring(start, end-start+1);
        }
        /* 
            Solution 3: 1) 假设每一个字母为分割线  babacd 找出长度为基数的以每个字母为中心的最长回文，
                        2) 每两个字母中间的空格为分割线 ba|bacd 找出长度为偶数的最长回文。
                        3）更新起始与终止位置。最后返回结果的substring
                        需要注意的就是其实位置与返回长度的时候index的处理。
                        Time complexity: O(n^2), space complexity:O(1)
        */
        public string LongestPalindrome(string s) {
            if(string.IsNullOrEmpty(s))
                return "";
            int start =0, end = 0;
            for(int i = 0; i<s.Length; i++)
            {
                int odd=GetPalindrom(s, i,i);
                int even = GetPalindrom(s,i,i+1);
                int len =Math.Max(odd,even);
                if(len>end-start+1)
                {
                    start = i-(len-1)/2;
                    end = i+len/2;
                }
            }
            return s.Substring(start, end-start+1);
        }
        public int GetPalindrom(string s, int left, int right)
        {
            while(left>=0 && right<s.Length && s[left]==s[right])
            {
                left--;
                right++;
            }
            return right-left-1;
        }

        //================================================================================//
        //----------6. ZigZag Conversion--Medium-----------------------------------------//
        /*
        The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows like this: (you may want to display this pattern in a fixed font for better legibility)

            P   A   H   N
            A P L S I I G
            Y   I   R
            And then read line by line: "PAHNAPLSIIGYIR"

            Write the code that will take a string and make this conversion given a number of rows:

            string convert(string s, int numRows);
            Example 1:

            Input: s = "PAYPALISHIRING", numRows = 3
            Output: "PAHNAPLSIIGYIR"
            Example 2:

            Input: s = "PAYPALISHIRING", numRows = 4
            Output: "PINALSIGYAHRPI"
            Explanation:

            P     I    N
            A   L S  I G
            Y A   H R
            P     I
         */

        public string Convert(string s, int numRows) {
            StringBuilder[] sbArray = new StringBuilder[numRows];
            for(int i=0; i<numRows; i++)
            {
                sbArray[i] = new StringBuilder();
            }
            int pos=0;
            while(pos<s.Length)
            {
                for(int i =0;i<numRows&&pos<s.Length; i++)
                {
                    sbArray[i].Append(s[pos]);
                    pos++;
                }
                for(int i=numRows-2; i>0&&pos<s.Length; i--)
                {
                    sbArray[i].Append(s[pos]);
                    pos++;
                }
            }
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<numRows; i++)
            {
                sb.Append(sbArray[i].ToString());
            }
            return sb.ToString();
        }
        //================================================================================//
        //----------7. Reverse Integern--Easy-----------------------------------------//
        /*
        Given a 32-bit signed integer, reverse digits of an integer.

        Example 1:

        Input: 123
        Output: 321
        Example 2:

        Input: -123
        Output: -321
        Example 3:

        Input: 120
        Output: 21
        Note:
        Assume we are dealing with an environment which could only store integers within the 32-bit
        signed integer range: [−231,  231 − 1]. For the purpose of this problem, assume that your 
        function returns 0 when the reversed integer overflows.
         */
        public int ReverseInteger(int x) 
        {
            int res =0;
            while(x!=0)
            {
                if(res>Int32.MaxValue/10 || (res==Int32.MaxValue/10&&x%10>7))
                    return 0;
                if(res<Int32.MinValue/10 || (res==Int32.MinValue/10&&x%10<-8))
                    return 0;
                res = res*10+x%10;
                x=x/10;
            }
            return res;
        }
        //================================================================================//
        //----------8. String to Integer (atoi)--Medium-----------------------------------------//
        /*
        这个题就不贴描述了。。主要就是corner case 的handle 当数值overflow的时候怎么办
         */
        public int MyAtoi(string str) 
        {
            if(string.IsNullOrEmpty(str))
                return 0;
            string trim = str.Trim();
            char[] chars = trim.ToCharArray();
            if(trim.Length==0)
                return 0;
            int pos=0;
            int res = 0;
            bool neg = false;
            if(!Char.IsDigit(chars[pos])&&chars[pos]!='-'&&chars[pos]!='+')
                    return 0;
            if(chars[pos]=='-')
            {
                neg = true;
                pos++;
            }
            else if(chars[pos]=='+')
            {
                pos++;
            }
            
            while(pos<trim.Length&&Char.IsDigit(chars[pos]))
            {
                if(res>Int32.MaxValue/10)
                {
                    if(neg)
                        return Int32.MinValue;
                    return Int32.MaxValue;
                }
                if(res==Int32.MaxValue/10)
                {
                    if(neg && chars[pos]-'0'>7)
                        return Int32.MinValue;
                    else if(!neg && chars[pos]-'0'>6)
                        return Int32.MaxValue;
                }
                res=res*10+(chars[pos]-'0');
                pos++;
            }
            return neg?-1*res :res;
        }
        //================================================================================//
        //----------9. Palindrome Number--Easy-----------------------------------------//
        /*
        Determine whether an integer is a palindrome. An integer is a palindrome when it reads the same backward as forward.

            Example 1:

            Input: 121
            Output: true
            Example 2:

            Input: -121
            Output: false
            Explanation: From left to right, it reads -121. From right to left, it becomes 121-. Therefore it is not a palindrome.
            Example 3:

            Input: 10
            Output: false
            Explanation: Reads 01 from right to left. Therefore it is not a palindrome.
         */
        public bool IsPalindromeNumber(int x) 
        {
            if(x<0)
                return false;
            int reverse = 0;
            int org = x;
            while(x!=0)
            {
                reverse=reverse*10+x%10;
                x = x/10;
            }
            return reverse==org;
        }
        //========================================================================================//
        //----------10. Regular Expression Matching--Hard-----------------------------------------//
        
        /*
        Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'.
            '.' Matches any single character.
            '*' Matches zero or more of the preceding element.
            The matching should cover the entire input string (not partial).

            Note:

            s could be empty and contains only lowercase letters a-z.
            p could be empty and contains only lowercase letters a-z, and characters like . or *.
            Example 1:

            Input:
            s = "aa"
            p = "a"
            Output: false
            Explanation: "a" does not match the entire string "aa".
            Example 2:

            Input:
            s = "aa"
            p = "a*"
            Output: true
            Explanation: '*' means zero or more of the precedeng element, 'a'. Therefore, by repeating 'a' once, it becomes "aa".
            Example 3:

            Input:
            s = "ab"
            p = ".*"
            Output: true
            Explanation: ".*" means "zero or more (*) of any character (.)".
            Example 4:

            Input:
            s = "aab"
            p = "c*a*b"
            Output: true
            Explanation: c can be repeated 0 times, a can be repeated 1 time. Therefore it matches "aab".
            Example 5:

            Input:
            s = "mississippi"
            p = "mis*is*p*."
            Output: false
         */

        //-----------------Notes--------------------------------------------------//
        /*
        Time Complexity O(m*n) where m is length of s, n is length of o.
        Space Complexity O(m*n)
        Dynamic Programing
        s="mississippi", p = "mis*is*p*."
        建立一个大小为 s.Length+1 X p.Length+1 的2D array dp[,].
        dp[0,0] always is true。 两个空string永远match。
        dp[0,1] always is false. 一个空string与一个字母的string永远不可能match。
        dp[0,2] is true only when p[2-1] is '*', 当p有两个字母只有当第二个字母为* 时候才能与空的s match。
        由此可以推出：
        dp[0,0] = true; dp[0,1] = false;
        dp[0, j] = p[i-1]=='*'&& dp[0,i-2] 

        分析 dp[i,j] 的情况
        dp[i, j]表示为在s的第i个字母的位置是否能与p的第j个字母位置是否可以match。
        这里如果换成index 则应该-1.

        1. s[i-1]==p[j-1] || p[j-1]=='.'
            这种情况下dp[i,j] 只取决于前一位是否match 
            dp[i,j] = dp[i-1,j-1];
        2. p[j-1]=='*'
            细分为
                如果*的前一位不等于‘，’或者不与s[i-1]相等 p[j-2]!=s[i-1] 
                    这种情况下* 的作用应该是删去前一位字母并且看dp[i,j-2]是否match
                其他情况 即 s[i-1]==p[j-2] || p[j-2]=='.'
                    当* 相当于多重复一次之前字母的时候
                        dp[i-1,j];
                        或者
                    当*相当于1即不代表任何字母的时候
                        dp[i,j-1];
                        或者
                    当*相当于0即删除前一个字母的时候
                        dp[i, j-2];
        3. 其他任何情况 dp【i，j】都为false
        */
        public bool IsMatch(string s, string p) 
        {
            bool[,] dp = new bool[s.Length+1, p.Length+1];
            dp[0,0]= true;
            if(p.Length>=1)
                dp[0,1] = false;
            for(int i =2; i<=p.Length; i++)
            {
                dp[0,i] = p[i-1]=='*'&&dp[0, i-2];
            }
            
            for(int i =1; i<=s.Length; i++)
            {
                for(int j=1; j<=p.Length; j++)
                {
                    if(s[i-1]==p[j-1]||p[j-1]=='.')
                    {
                        dp[i,j]=dp[i-1,j-1];
                    }
                    else if(j>1 && p[j-1]=='*')
                    {
                        if( p[j-2]!=s[i-1] && p[j-2]!='.')
                        {
                            dp[i, j] = dp[i, j-2];
                        }
                        else
                        {
                            dp[i, j] = dp[i-1, j]||dp[i, j-1]|| dp[i, j-2];
                        }
                    }
                }
            }
            return dp[s.Length, p.Length];
        }
        //========================================================================================//
        //----------11. Container With Most Water--Medium-----------------------------------------//
        /*
        Given n non-negative integers a1, a2, ..., an , where each represents a point at coordinate (i, ai).
         n vertical lines are drawn such that the two endpoints of line i is at (i, ai) and (i, 0). 
         Find two lines, which together with x-axis forms a container, such that the container contains the most water.

        Note: You may not slant the container and n is at least 2.

        Example:

        Input: [1,8,6,2,5,4,8,3,7]
        Output: 49
 
         */

         /*-----------------------Notes----------------------------------------------- */
         /*
         Time Complexity: O(n), Space complexity: O(1)
         Two pointer
          */
        public int MaxArea(int[] height) 
        {
            if(height==null|| height.Length==0)
                return 0;
            int p1=0, p2= height.Length-1;
            int res = 0;
            while(p1<p2)
            {
                res = Math.Max(Math.Min(height[p1], height[p2])*(p2-p1), res);
                if(height[p1]<height[p2])
                    p1++;
                else
                    p2--;
            }
            return res;
        }
        //========================================================================================//
        //----------14. Longest Common Prefix--Easy-----------------------------------------------//
        /*
        Write a function to find the longest common prefix string amongst an array of strings.

        If there is no common prefix, return an empty string "".

        Example 1:

        Input: ["flower","flow","flight"]
        Output: "fl"
        Example 2:

        Input: ["dog","racecar","car"]
        Output: ""
        Explanation: There is no common prefix among the input strings.
        */
        public string LongestCommonPrefix(string[] strs) 
        {
            if(strs==null||strs.Length==0)
                return "";
            StringBuilder sb = new StringBuilder();
            for(int i = 0;i<strs[0].Length; i++)
            {
                for(int j = 0; j<strs.Length; j++)
                {
                    if(i>=strs[j].Length  || strs[j][i]!=strs[0][i])
                    {
                        return sb.ToString();
                    }
                }
                sb.Append(strs[0][i]);
            }
            return sb.ToString();
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
        //========================================================================================//
        //----------21.  Merge Two Sorted Lists--Easy---------------------------------------------//
        /*
        Merge two sorted linked lists and return it as a new list. The new list should be made by 
        splicing together the nodes of the first two lists.

        Example:

        Input: 1->2->4, 1->3->4
        Output: 1->1->2->3->4->4
        */
        public ListNode MergeTwoLists(ListNode l1, ListNode l2) 
        {
            ListNode dummy = new ListNode(0);
            ListNode node = dummy;
            while(l1!=null || l2!=null)
            {
                int a = l1==null?Int32.MaxValue:l1.val;
                int b = l2==null?Int32.MaxValue:l2.val;
                if(a>b)
                {
                    node.next = l2;
                    l2=l2.next;
                }
                else
                {
                    node.next = l1;
                    l1=l1.next;
                }
                node = node.next;
            }
            return dummy.next;
        }
        //========================================================================================//
        //----------22.  Generate Parentheses--Medium---------------------------------------------//
        /*
        Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.

        For example, given n = 3, a solution set is:

        [
        "((()))",
        "(()())",
        "(())()",
        "()(())",
        "()()()"
        ]
        */

        /*----------------------------Notes-----------------------------------------------------*/
        /*
        1. Choice: Place a '(' or a ')'
        2. Constraints: We cannot place a '(' when the total number of '(' is equal to n;
                        we cannot place a ')' when the number of ')' is equal to '(';
        3. Goal: Form a string length is n*2
        Back tracking:
        n=3                open 0  close 0
                                 |
                              1, 0 "("
                     |                      |
                 2,0 "(("                   1,1 "()"
            |              |                      |
        3,0 "((("        2,1 "(()"              2,1 "()("
            |           |          |          |            |
    3,1 "((()"     3,1 "(()("  2,2 "(())"  3,1 "()(("    2,2 "()()"
        |            |             |           |             |
    3,2 "((())"   3,2 "(()()"  3,2"(())("   3,2 "()(()"  3,2 "()()("
       |             |             |           |              |
    3,3 "((()))"  3,3 "(()())" 3,3"(())()"  3,3"()(())"  3,3 "()()()"
         */
        public IList<string> GenerateParenthesis(int n) 
        {
            IList<string> res = new List<string>();
            StringBuilder sb = new StringBuilder();
            HelperGenerateParenthesis(res, sb, 0, 0, n);
            return res;
        }
        public void HelperGenerateParenthesis(IList<string> res, StringBuilder sb, int open, int close, int n)
        {
            if(sb.Length==n*2)
            {
                res.Add(sb.ToString());
                return;
            }
            if(open<n)
            {
                sb.Append('(');
                HelperGenerateParenthesis(res, sb, open+1, close, n);
                sb.Remove(sb.Length-1, 1);
            }
            if(close<open)
            {
                sb.Append(')');
                HelperGenerateParenthesis(res, sb, open, close+1, n);
                sb.Remove(sb.Length-1, 1);
            }
        }
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
        /*
        Time complexity: O(nlogn) Space Complexity: O(k)
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
        //========================================================================================//
        //----------24.  Swap Nodes in Pairs--Medium----------------------------------------------//
        /*
        Given a linked list, swap every two adjacent nodes and return its head.

        Example:

        Given 1->2->3->4, you should return the list as 2->1->4->3.
        Note:

        Your algorithm should use only constant extra space.
        You may not modify the values in the list's nodes, only nodes itself may be changed.
         */
        public ListNode SwapPairs(ListNode head) 
        {
            ListNode dummy = new ListNode(0);
            dummy.next = head;
            ListNode prev = dummy;
            ListNode node = head;
            while(node!=null && node.next!=null)
            {
                ListNode temp = node.next.next;
                prev.next = node.next;
                node.next = temp;
                prev.next.next = node;
                prev = node;
                node = node.next;
            }
            return dummy.next;
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
        //========================================================================================//
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
        //========================================================================================//
        //----------30. Substring with Concetenation of all words--Hard---------------------------// 
        /*
            You are given a string, s, and a list of words, words, that are all of the same length.
            Find all starting indices of substring(s) in s that is a concatenation of each word in words 
            exactly once and without any intervening characters.

            Example 1:

            Input:
            s = "barfoothefoobarman",
            words = ["foo","bar"]
            Output: [0,9]
            Explanation: Substrings starting at index 0 and 9 are "barfoor" and "foobar" respectively.
            The output order does not matter, returning [9,0] is fine too.
            Example 2:

            Input:
            s = "wordgoodgoodgoodbestword",
            words = ["word","good","best","word"]
            Output: []
         */
        public IList<int> FindSubstring(string s, string[] words) 
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            IList<int> res = new List<int>();
            if(string.IsNullOrEmpty(s)|| words==null || words.Length==0)
                return res;
            foreach(string word in words)
            {
                if(map.ContainsKey(word))
                {
                    map[word]++;
                }
                else
                    map.Add(word,1);
            }
            int len = words[0].Length;
            for(int i = 0; i<s.Length; i++)
            {
                Dictionary<string, int> copy = new Dictionary<string,int>();
                bool breaked = false;
                for(int j=0; j<words.Length; j++)
                {
                    if(i+j*len+len<=s.Length)
                    {
                        string sub = s.Substring(i+j*len,len);
                        if(map.ContainsKey(sub))
                        {
                            if(copy.ContainsKey(sub))
                            {
                                if(copy[sub]==map[sub])
                                {
                                    breaked = true;
                                    break;
                                }
                                copy[sub]++;
                            }
                            else
                            {
                                copy.Add(sub,1);
                            }
                        }
                        else
                        {
                            breaked = true;
                            break;
                        }
                    }
                    else
                        return res;

                }
                if(!breaked)
                {
                    res.Add(i);
                }
            }
            return res;
        }
        //========================================================================================//
        //----------31. Next Permutation--Medium--------------------------------------------------// 
        /*
            Implement next permutation, which rearranges numbers into the lexicographically next greater permutation of numbers.

            If such arrangement is not possible, it must rearrange it as the lowest possible order (ie, sorted in ascending order).

            The replacement must be in-place and use only constant extra memory.

            Here are some examples. Inputs are in the left-hand column and its corresponding outputs are in the right-hand column.

            1,2,3 → 1,3,2
            3,2,1 → 1,2,3
            1,1,5 → 1,5,1
         */
         /*-----------------------Notes------------------------------------------------------------*/
         /*
         Think about how permutation works using backtracking.
         1,2,3,4-> 1,2,4,3 ->1,3,2,4->1,3,4,2->1,4,2,3->1,4,3,2->2,1,3,4->2,1,4,3->2,3,1,4->2,3,4,1->2,4,1,3->2,4,3,1
               |       |           |      |          |    |            |      |          |      |          |    |

         ->3,1,2,4->3,1,4,2->3,2,1,4->3,2,4,1->3,4,1,2->3,4,2,1->4,1,2,3->4,1,3,2->4,2,1,3->4,2,3,1->4,3,1,2->4,3,2,1
                 |      |          |      |          |    |            |      |          |      |          |    |
           
          The marked place is where the end of increasing sequence from the end.
          1. we can see that if 1,2,4,3 means this is the last sequence that start with 1,2 the next sequence should start with 1,3
            and should be all ordered by ascending after 3 which is 1,3,2,4 is the first permutation that start with 1,3
            
            same 1,4,3,2 means the last permutation start from 1. 
        2. to find the next permutation of 1,4,3,2 we need to first reverse the desending sequence to 1,2,3,4 
        3. then we need to switch 1 with the first element greater than 1 , so the result is 2,1,3,4 
           we can see that the result after 2 is all ascending which is the first permutation starts with 2.
        
        ex 3,4,2,1 ->3,1,2,4 ->swap 3 and 4 ->4,1,2,3
          */
        public void NextPermutation(int[] nums) 
        {
            int swapPos = nums.Length-2;
            //find the index from the end that is not descending 
            while(swapPos>=0)
            {
                if(nums[swapPos]>=nums[swapPos+1])
                {
                    swapPos--;
                }
                else
                    break;
            }
            //reverse the descending sequence
            Reverse(nums, swapPos+1, nums.Length-1);
            //if not all descending swap the pos with the first one that is greater than it .
            if(swapPos>=0)
            {
                int pos = swapPos+1;
                while(nums[pos]<=nums[swapPos])
                {
                    pos++;
                }
                Swap(nums, swapPos, pos);
            }
        }
        private void Swap(int[] nums, int a, int b)
        {
            int temp = nums[a];
            nums[a] = nums[b];
            nums[b] = temp;
        }
        private void Reverse(int[] nums, int start, int end)
        {
            while(start<end)
            {
                Swap(nums, start, end);
                start++;
                end--;
            }
        }
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
        //========================================================================================//
        //----------33. Find First and Last Position of Element in Sorted Array--Medium-----------// 
        /*
            Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.

            (i.e., [0,1,2,4,5,6,7] might become [4,5,6,7,0,1,2]).

            You are given a target value to search. If found in the array return its index, otherwise return -1.

            You may assume no duplicate exists in the array.

            Your algorithm's runtime complexity must be in the order of O(log n).

            Example 1:

            Input: nums = [4,5,6,7,0,1,2], target = 0
            Output: 4
            Example 2:

            Input: nums = [4,5,6,7,0,1,2], target = 3
            Output: -1
         */
        public int Search(int[] nums, int target) 
        {
            if(nums==null|| nums.Length==0)
                return -1;
            int start = 0;
            int end = nums.Length-1;
            while(start+1<end)
            {
                int mid = start+(end-start)/2;
                if(nums[mid]>=nums[0])
                {
                    if(target<nums[mid] && target>=nums[0])
                        end = mid;
                    else
                    {
                        start = mid;
                    }
                }
                else
                {
                    if(target>nums[mid] && target<=nums[nums.Length-1])
                    {
                        start= mid;
                    }
                    else
                        end = mid;
                }
            }
            if(nums[start] ==target)
                return start;
            if(nums[end] == target)
                return end;
            return -1;
        }

        //========================================================================================//
        //----------34. Find First and Last Position of Element in Sorted Array--Medium-----------// 
        /*
        Given an array of integers nums sorted in ascending order, find the starting and ending position of a given target value.

        Your algorithm's runtime complexity must be in the order of O(log n).

        If the target is not found in the array, return [-1, -1].

        Example 1:

        Input: nums = [5,7,7,8,8,10], target = 8
        Output: [3,4]
        Example 2:

        Input: nums = [5,7,7,8,8,10], target = 6
        Output: [-1,-1]
         */
        public int[] SearchRange(int[] nums, int target) 
        {
            if(nums==null || nums.Length ==0)
                return new int[2]{-1,-1};
            int[] res = new int[2]; 
            res[0] = BinarySearch(nums, target, true);
            res[1] = BinarySearch(nums, target, false);
            return res;
        }
        private int BinarySearch(int[] nums, int target, bool isFirst)
        {
            int start = 0; int end = nums.Length-1;
            while(start+1<end)
            {
                int mid = start+(end-start)/2;
                if(isFirst)
                {
                    if(nums[mid]<target)
                    {
                        start = mid;
                    }
                    else
                    {
                        end = mid;
                    }
                }
                else
                {
                    if(nums[mid]<=target)
                    {
                        start = mid;
                    }
                    else
                    {
                        end = mid;
                    }
                }
            }
            if(nums[start]==target&& nums[end]==target)
            {
                if(isFirst)
                    return start;
                return end;
            }
            else if(nums[start]==target)
                return start;
            else if(nums[end]==target)
                return end;
            return -1;

        }
        //========================================================================================//
        //----------35. Search Insert Position--Easy----------------------------------------------// 
        /*
            Given a sorted array and a target value, return the index if the target is found. If not, return the index where it would be if it were inserted in order.

            You may assume no duplicates in the array.

            Example 1:

            Input: [1,3,5,6], 5
            Output: 2
            Example 2:

            Input: [1,3,5,6], 2
            Output: 1
            Example 3:

            Input: [1,3,5,6], 7
            Output: 4
            Example 4:

            Input: [1,3,5,6], 0
            Output: 0
         */
        public int SearchInsert(int[] nums, int target) 
        {
            if(nums==null || nums.Length==0)
                return 0;
            int start = 0, end = nums.Length-1;
            while(start+1<end)
            {
                int mid = start+(end-start)/2;
                if(nums[mid]<target)
                {
                    start = mid;
                }
                else
                    end = mid;
            }
            if(nums[start]==target)
                return start;
            else if(nums[start]<target&&nums[end]>=target)
                return end;
            else if(nums[end]<target)
                return nums.Length;
            else 
                return 0;
        }
        //========================================================================================//
        //----------36. Valid Soduku--Medium------------------------------------------------------// 
        public bool IsValidSudoku(char[,] board)
        {
            List<HashSet<char>> subBoxes = new  List<HashSet<char>>() ;
            subBoxes.Add(new HashSet<char>());
            subBoxes.Add(new HashSet<char>());
            subBoxes.Add(new HashSet<char>());
            for(int i =0; i<9; i++)
            {
                HashSet<char> verSet = new HashSet<char>();
                HashSet<char> horiSet = new HashSet<char>();
                if(i==3 || i==6)
                {
                    subBoxes[0].Clear();
                    subBoxes[1].Clear();
                    subBoxes[2].Clear();
                }
                for(int j=0; j<9;j++)
                {
                    if(board[i,j]!='.')
                    {
                        if(!verSet.Add(board[i,j]))
                            return false;
                        if(!subBoxes[j/3].Add(board[i,j]))
                        {
                            return false;
                        }
                    }
                    if(board[j,i]!='.')
                    {
                        if(!horiSet.Add(board[j,i]))
                            return false;
                    }
                }
            }
            return true;
        }

        
    }
}
