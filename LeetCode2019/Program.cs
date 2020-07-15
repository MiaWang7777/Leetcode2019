using System;
using LeetCode2019.Shared;
using LeetCode2019.ByOrder;
namespace LeetCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Top51_100 sol = new Top51_100();
            TreeNode p = new TreeNode(1);
            p.left = new TreeNode(2);
            p.right = new TreeNode(3);
            TreeNode q = new TreeNode(1);
            q.left = new TreeNode(2);
            q.right = new TreeNode(3);

            bool res = sol.IsSameTreeIterative(p, q);
        }
        public static void ShiftLeft(int[] arr, int i)
        {
            if(arr==null || arr.Length==0)
                throw new ArgumentNullException();
            i = i%arr.Length;
            
            int p1 = 0; int p2 = i;
            while(p1<p2)
            {
                Swap(arr, p1, p2);
                if(p2<arr.Length-1)
                    p2++;
                p1++;
            }
        }
        private static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}
