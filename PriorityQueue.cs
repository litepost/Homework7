/**
 * This class implements a min heap with nodes containing a priority value
 * and its corresponding object. In this program Object is a Vertex object.
 * Otherwise, the code closely follows homework 5.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Homework7 {
    class PriorityQueue<T> {

        class Node {
            public int Priority { get; set; }
            public T Object { get; set; }
        }

        private const int ROOT = 0;
        private List<Node> queue = new List<Node>();
        int heapSize = -1;
        public int count { get { return queue.Count; } }

        public PriorityQueue() {

        }

        private void swap(int i, int j) {
            Node temp = queue[i];
            queue[i] = queue[j];
            queue[j] = temp;
        }

        private int parent(int index) {
            return (index - 1) / 2;
        }
        private int leftChild(int parent) {
            return parent * 2 + 1;
        }

        private int rightChild(int parent) {
            return leftChild(parent) + 1;
        }

        private bool hasLeft(int parent) {
            return leftChild(parent) <= heapSize;
        }

        private bool hasRight(int parent) {
            return rightChild(parent) <= heapSize;
        }

        public void Enqueue(int priority, T obj) {
            Node node = new Node() { Priority = priority, Object = obj};
            queue.Add(node);
            heapSize++;
            percolateUp(heapSize);
        }

        private void heapify() {
            //transforms an array into a heap
            for (int i = heapSize / 2; i >=ROOT; i--) {
                percolateDown(i);
            }
        }

        public T Dequeue() {
            //swap the root with the last element
            //then remove the last element/old root
            //re-heapify
            //return old root
            if (heapSize == -1)
                throw new InvalidOperationException("The root cannot be removed because the array is empty");

            Node oldRoot = queue[ROOT];
            swap(ROOT, heapSize);
            queue.RemoveAt(heapSize);
            heapSize--;
            percolateDown(ROOT);
            return oldRoot.Object;
        }

        private void percolateDown(int index) {
            if (hasLeft(index)) {
                int childIndex = leftChild(index);
                if (hasRight(index)) {
                    int rightChildIndex = rightChild(index);
                    if (queue[rightChildIndex].Priority < queue[childIndex].Priority) {
                        childIndex = rightChildIndex;
                    }
                }

                if (queue[childIndex].Priority < queue[index].Priority) {
                    swap(childIndex, index);
                    percolateDown(childIndex);
                }
            }
        }
        private void percolateUp(int index) {
            if (index > ROOT) {
                int p = parent(index);
                if (queue[index].Priority < queue[p].Priority) {
                    swap(index, p);
                    percolateUp(p);
                }
            }
        }

        public void UpdatePriority(T obj, int priority) {
            for (int i = 0; i <= heapSize; i++) {
                Node node = queue[i];
                if (object.ReferenceEquals(node.Object, obj)) {
                    node.Priority = priority;
                    percolateUp(i);
                    percolateDown(i);
                }
            }
        }
        public override string ToString() { 
            int i = 0;
            StringBuilder sb = new StringBuilder();

            foreach (Node n in queue) {
                sb.AppendLine($"Node: {i}\t Priority: {n.Priority}");
                i++;
            }

            return sb.ToString();
        }

        
    }
}
