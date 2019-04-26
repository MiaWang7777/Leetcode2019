
namespace LeetCode2019.EPI
{
    public class PrimitiveTypes
    {
        //---------------------4.1----------------------------------//

        //LeetCode 191. Number of 1 Bits--Easy-----------------------//
        /*Count nuber of 1s in the bits */
        /* ---------------Notes-------------------------------------- */
        /*
            n&1 will get wether the last bit is 1.
            and then shift the number to right 1 bit continue the loop until
            n is 0.
         */
        public int HammingWeight(uint n) 
        {
            int count =0;
            while(n!=0)
            {
                count+=((int)n&1);
                n>>=1;
            }
            return count;
        }
    }
}