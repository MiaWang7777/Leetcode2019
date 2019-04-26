using System;

namespace LeetCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[]{1,2,3,4,5,6,7,8};
            ShiftLeft(arr, 3);
            foreach(var a in arr)
            {
                Console.WriteLine(a+"/");
            }
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
