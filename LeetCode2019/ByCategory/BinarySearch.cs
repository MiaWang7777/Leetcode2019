using System;
using System.Collections.Generic;
using LeetCode2019.Shared;


namespace LeetCode2019.ByCategory
{
    public class BinarySearch
    {
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