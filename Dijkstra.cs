using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework7 {

    public class Vertex {
        public string Name { get; set; }
        public int Distance { get; set; }

        public Vertex Parent { get; set; }
    }


    public class Dijkstra {

        private int[,] adjMatrix;
        private List<Vertex> vertices;

        public Dijkstra(int[,] adj) {
            if (adj is null)
                throw new ArgumentNullException("The graph cannot be null");
            if (adj.Length == 0)
                throw new ArgumentException("The graph must have at least 1 node");

            //deep copy of adjacency matrix
            adjMatrix = new int[adj.GetLength(0), adj.GetLength(1)];
            for (int i = 0; i < adj.GetLength(0); i++) {
                for (int j = 0; j < adj.GetLength(1); j++) {
                    adjMatrix[i, j] = adj[i, j];
                }
            }

            vertices = new List<Vertex>();
            for (int i = 0; i < adj.GetLength(0); i++) {
                vertices.Add(new Vertex() { Name = i.ToString(), Distance = int.MaxValue, Parent = null });
            }
        }


        public List<Vertex> ShortestPath(int source) {
            List<Vertex> result = new List<Vertex>();
            if (vertices.Count == 0)
                return result;

            vertices[source].Distance = 0;

            try {
                PriorityQueue<Vertex> queue = new PriorityQueue<Vertex>();
                foreach (Vertex v in vertices) {
                    queue.Enqueue(v.Distance, v);
                }

                while (queue.count > 0) {
                    var u = queue.Dequeue();
                    result.Add(u);
                    
                    for (int v = 0; v < adjMatrix.GetLength(0); v++) {
                        int uIndex = Int32.Parse(u.Name);
                        if (adjMatrix[uIndex, v] > 0) {
                            if (vertices[v].Distance > u.Distance + adjMatrix[uIndex, v]) {
                                vertices[v].Distance = u.Distance + adjMatrix[uIndex, v];
                                vertices[v].Parent = u;
                            }
                            queue.UpdatePriority(vertices[v], vertices[v].Distance);
                        }
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                throw new ArithmeticException("Check console for details");
            }
            
            return result;
        }

        public string PrintShortestPath(int origin, int dest) {
            if (vertices.Count == 0)
                return "N/A";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Shortest path from {origin} to {dest}");
            sb.AppendLine("");
            sb.AppendLine(PrintPathHelper(vertices[origin], vertices[dest]).ToString());
            return sb.ToString();
        }

        private StringBuilder PrintPathHelper(Vertex origin, Vertex dest) {
            StringBuilder sb = new StringBuilder();

            if (origin == dest) 
                sb.AppendLine($"Vertex: {dest.Name}\tTotal Distance: {dest.Distance}");
            else {
                sb = PrintPathHelper(origin, dest.Parent);
                sb.AppendLine($"Vertex: {dest.Name}\tTotal Distance: {dest.Distance}");
            }

            return sb;
        }

        public string PrintAdjacencyMatrix() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Adjacency Matrix:");

            for (int i = 0; i < adjMatrix.GetLength(0); i++) {
                int[] arr = Enumerable.Range(0, adjMatrix.GetLength(1)).Select(x => adjMatrix[i, x]).ToArray();
                sb.AppendJoin(", ", arr);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
