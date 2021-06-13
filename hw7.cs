/**
 * Author: Jeff Leupold
 * Homework 7 - Dijkstra's algorithm
 * Due Date: 2021-06-03
 */

/**
 * Test cases: I created a function that randomly generates adjacency matrices.
 * I found that at high values of weights, then the program crashes from integer overflow
 * so I capped it (int.MaxValue - 1) / 2 in case there are 2 max weights added together.
 * Below in Main(), I left a test case that I used to test the priority queue, which is 
 * one of the test cases used in homework 5. 
 * 
 * There is also a singular test case in the comments in Main() to test Dijkstra's algorithm.
 * I found it online and my answer matches the site's answer. Otherwise, I ran my random test cases 
 * and validated them myself. I did change my range to <= 100 for weights so that the numbers 
 * are easier to read and calculate. Test cases included 1x1 as well as 17x17.
 */

using System.Collections.Generic;
using System;

namespace Homework7 {
    class hw7 {
        static void Main(string[] args) {
            Console.WriteLine("========== Dijkstra's Algorithm ==========");

            //******test if PriorityQueue operates as expected******
            /*PriorityQueue<int> testQueue = new PriorityQueue<int>();

            int[] input5 = new int[] { 9, 44, -1, 23, 99, 0, -12 };

            for (int i = 0; i < input5.Length; i++) {
                testQueue.Enqueue(input5[i], input5[i]);
            }

            Console.WriteLine(testQueue.ToString());
            int root = testQueue.Dequeue();
            Console.WriteLine(testQueue.ToString());
            root = testQueue.Dequeue();
            Console.WriteLine(testQueue.ToString());
            root = testQueue.Dequeue();
            Console.WriteLine(testQueue.ToString());

            testQueue.Enqueue(5, 5);
            testQueue.Enqueue(-1, -1);
            Console.WriteLine(testQueue.ToString());*/

            //****** test case found on the Internet ******
            /*int[,] adjacencyMatrix = {{ 0,0,0,3,12 },
                                      { 0,0,2,0,0 },
                                      { 0,0,0,-2,0 },
                                      { 0,5,3,0,0 },
                                      { 0,0,7,0,0 }};
            Dijkstra d1 = new Dijkstra(adjacencyMatrix);
            Console.WriteLine(d1.PrintAdjacencyMatrix());
            List<Vertex> result = d1.ShortestPath(0);
            Console.WriteLine(d1.PrintPath(0, 2));*/

            List<int[,]> tests = TestCaseGenerator(5, 17);
            var rand = new Random();
            int caseNum = 1;
            int source = 0;

            foreach (var t in tests) {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"*** Test Case {caseNum++} ***");
                try {
                    Dijkstra d = new Dijkstra(t);
                    Console.WriteLine(d.PrintAdjacencyMatrix());
                    source = rand.Next(t.GetLength(0));
                    List<Vertex> result = d.ShortestPath(source);
                    Console.WriteLine(d.PrintShortestPath(source, rand.Next(1, t.GetLength(0))));
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("========== EXIT SUCCESS! ==========");
        }

        public static List<int[,]> TestCaseGenerator(int NumberOfTestCases, int MaxSizeOfMatrix) {
            List<int[,]> testBank = new List<int[,]>();

            for (int testCase = 0; testCase < NumberOfTestCases; testCase++) {
                var rand = new Random();
                int size = rand.Next(MaxSizeOfMatrix+1);
                int[,] testMatrix = new int[size, size];

                for (int i = 0; i < size; i++) {
                    for (int j = 0; j < size; j++) {
                        if (i == j)
                            testMatrix[i, j] = 0;
                        else
                            testMatrix[i, j] = rand.Next((int.MaxValue-1) / 2);
                        //testMatrix[i, j] = rand.Next(100);
                        //testMatrix[i, j] = rand.Next(int.MaxValue / 2);
                    }
                }
                testBank.Add(testMatrix);
            }
            return testBank;
        }
    }
}
