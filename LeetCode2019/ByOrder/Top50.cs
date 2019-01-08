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
    
    }
}
