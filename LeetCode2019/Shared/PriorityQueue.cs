using System.Collections.Generic;

namespace LeetCode2019.Shared
{
    public class PriorityQueue<T> 
    {
        private IList<T> list;
        public int Count;
        private IComparer<T> comparer;
        
        public PriorityQueue(IComparer<T> comparer) {
            list = new List<T>();
            this.comparer = comparer;
        }
        
        public void Push(T item) {
            list.Add(item);
            Count++;
            MoveUp(list.Count - 1);
        }
        public T Pop() {
            T res = list[0];
            list[0] = list[Count - 1];
            list.RemoveAt(Count - 1);
            Count--;
            if (Count > 1) {
                MoveDown(0);
            }
            return res;
        }
        public int Size()
        {
            return Count;
        }
        private void MoveUp(int curr) {
            while (curr > 0) {
                int parent = (curr - 1) / 2;
                if (comparer.Compare(list[curr], list[parent]) < 0) {
                    Swap(curr, parent);
                    curr = parent;
                }
                else {
                    break;
                }
            }
        }
        
        private void MoveDown(int curr) {
            while (curr < list.Count - 1) {
                int left = curr * 2 + 1, right = curr * 2 + 2;
                int original = curr;
                if (left < list.Count) {
                    if (comparer.Compare(list[left], list[curr]) < 0) {
                        curr = left;
                    }
                    if (right < list.Count && comparer.Compare(list[right], list[curr]) < 0) {
                        curr = right;
                    }
                    if (curr != original) {
                        Swap(curr, original);
                    }
                    else {
                        break;
                    }
                }
                else {
                    break;
                }
            }
        }
        
        private void Swap(int a, int b) 
        {
            T temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }
    }
}