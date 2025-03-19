using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace PerformanceTest
{
    public class RedBlackTreePerformanceTests
    {
        private RedBlackTree<int> tree;

        [GlobalSetup]
        public void Setup()
        {
            tree = new RedBlackTree<int>();
        }

        [Benchmark]
        public void InsertTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                tree.Insert(i);
            }
        }

        [Benchmark]
        public void DeleteTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                tree.Insert(i);
            }

            for (int i = 0; i < 10000; i++)
            {
                tree.Delete(i);
            }
        }

        [Benchmark]
        public void SearchTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                tree.Insert(i);
            }

            for (int i = 0; i < 10000; i++)
            {
                tree.Search(i);
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<RedBlackTreePerformanceTests>();
        }
    }
}