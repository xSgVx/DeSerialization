using System;
using System.IO;
using System.Text;

namespace DeSerialize
{
    class Program
    {
        class ListNode
        {
            public ListNode Previous;
            public ListNode Next;
            //public ListNode Random;
            public string Data;
        }

        class ListRandom
        {
            public ListNode Head;
            public ListNode Tail;
            public int Count;
            public void Serialize(Stream s)
            {
                var node = Head;

                while (node != null)
                {
                    s.Write(Encoding.ASCII.GetBytes(node.Data));

                    if (node != Tail)
                    {
                        s.Write(Encoding.ASCII.GetBytes(" "));
                    }

                    node = node.Next;
                }
            }
            public void Deserialize(Stream s)
            {
                StreamReader reader = new StreamReader(s);

                var str = reader.ReadToEnd();

                var allNodeData = str.Split(" ");

                ListRandom listRandom = new ListRandom();
                ListNode prevNode = null;

                foreach (var nodeData in allNodeData)
                {
                    listRandom.Count++;
                    ListNode node = new ListNode() { Data = nodeData };

                    if (listRandom.Head == null)
                        listRandom.Head = node;

                    node.Previous = prevNode;

                    if (prevNode != null)
                        prevNode.Next = node;

                    prevNode = node;

                }

                listRandom.Tail = prevNode;
            }
        }

        static void Main(string[] args)
        {
            var testNode3 = new ListNode();
            var testNode2 = new ListNode();
            var testNode1 = new ListNode();

            testNode3.Previous = testNode2;
            testNode2.Previous = testNode1;
            testNode1.Next = testNode2;
            testNode2.Next = testNode3;

            testNode3.Data = "somedata3";
            testNode2.Data = "somedata2";
            testNode1.Data = "somedata1";

            var listRandom = new ListRandom();
            listRandom.Head = testNode1;
            listRandom.Tail = testNode3;

            var file = File.Create("test");

            listRandom.Serialize(file);
            file.Close();

            file = File.OpenRead("test");
            listRandom.Deserialize(file);







        }
    }
}
