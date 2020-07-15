using System;
using System.Collections.Generic;
using System.Text;
using LeetCode2019.Shared;
namespace LeetCode2019.ByOrder
{
    public class Top151_200
    {
        //========================================================================================//
        //----------200. Number of Islands--Medium------------------------------------------------//
        /*
            Given a 2d grid map of '1's (land) and '0's (water), count the number of islands. An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. You may assume all four edges of the grid are all surrounded by water.

            Example 1:

            Input:
            11110
            11010
            11000
            00000

            Output: 1
         */
         /// <summary>
         /// BFS
         /// Time Complexity: O(mn), Space Complexity: O(min[m,n])
         /// </summary>
        public int NumIslandsBFS(char[][] grid) 
        {
            if(grid.Length==0|| grid[0].Length==0)
                return 0;
            Queue<int[]> queue = new Queue<int[]>();
            int count = 0;
            int m = grid.Length;
            int n = grid[0].Length;
            int[,] direction = new int[,]{{1,0,-1,0},{0,1,0,-1}};
            for(int i =0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if(grid[i][j]=='1')
                    {
                        count++;
                        queue.Enqueue(new int[]{i,j});
                        grid[i][j] = '0';
                        while(queue.Count>0)
                        {
                            int[] cur = queue.Dequeue();
                            
                            for(int k=0; k<4; k++)
                            {
                                int a = cur[0]+direction[0,k];
                                int b = cur[1]+direction[1,k];
                                if(InBound(a, b,m,n) && grid[a][b]=='1')
                                {
                                    queue.Enqueue(new int[]{a,b});
                                    grid[a][b] = '0';
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }
        /// <summary>
        /// DFS Iterative
        /// Time Complexity: O(mn), Space Complexity: O(mn)
        /// </summary>
        public int NumIslandsDFSIterative(char[][] grid) 
        {
            if(grid.Length==0|| grid[0].Length==0)
                return 0;
            Stack<int[]> stack = new Stack<int[]>();
            int count = 0;
            int m = grid.Length;
            int n = grid[0].Length;
            int[,] direction = new int[,]{{1,0,-1,0},{0,1,0,-1}};
            for(int i =0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if(grid[i][j]=='1')
                    {
                        count++;
                        stack.Push(new int[]{i,j});
                        grid[i][j] = '0';
                        while(stack.Count>0)
                        {
                            int[] cur = stack.Pop();
                            
                            for(int k=0; k<4; k++)
                            {
                                int a = cur[0]+direction[0,k];
                                int b = cur[1]+direction[1,k];
                                if(InBound(a, b,m,n) && grid[a][b]=='1')
                                {
                                    stack.Push(new int[]{a,b});
                                    grid[a][b] = '0';
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// DFS Recursion 
        /// Time Complexity: O(mn), Space Complexity: O(mn)
        /// </summary>
        public int NumIslandsDFS(char[][] grid) 
        {
            if(grid.Length==0|| grid[0].Length==0)
                return 0;
            int count = 0;
            int m = grid.Length;
            int n = grid[0].Length;
        
            for(int i =0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if(grid[i][j]=='1')
                    {
                        count++;
                        Dfs(grid,i,j);
                    }
                }
            }
            return count;
        }

        private void Dfs(char[][] grid, int a, int b)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            int[,] direction = new int[,]{{1,0,-1,0},{0,1,0,-1}};
            
            //End condition of recursion
            if(!InBound(a, b,m,n) || grid[a][b]=='0')
                return;
        
            grid[a][b] = '0';
            for(int k=0; k<4; k++)
            {
                int c = a+direction[0,k];
                int d = b+direction[1,k];
                Dfs(grid, c, d);
            }
            
        }
        private bool InBound(int a, int b, int m, int n)
        {
            if(a>=0&& a<m && b>=0&& b<n)
                return true;
            return false;
        }
    }
}