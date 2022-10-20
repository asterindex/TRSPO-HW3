using System;
using System.Collections.Generic;

namespace TRSPO_HW3
{
    
    
    class MyQueue<T> {

        private class Node {
            public T data;
            public Node(T data) {
                this.data = data;
            }
            public Node() {

            }
        }
        Node[] nodes;
        int current;
        int emptySpot;

        public Int64 Count
        {
            get;
            set;
        }
        public MyQueue(int size) {
            nodes = new Node[size];
            for (int i = 0; i < size; i++) {
                nodes[i] = new Node();
            }
            this.current = 0;
            this.emptySpot = 0;
        }

        public void Enqueue(T value){
            nodes[emptySpot].data = value;
            emptySpot++;
            if (emptySpot >= nodes.Length) {
                emptySpot = 0;
            }

            Count++;
        }
        public T Dequeue()
        {
            Count--;
            int ret = current;
            current++;
            if (current >= nodes.Length) {
                current = 0;
            }
            return nodes[ret].data;
        }
    }
}