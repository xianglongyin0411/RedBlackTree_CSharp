using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Running;

namespace PerformanceTest
{
    [Config(typeof(ConfigWithMedian))]
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
            // 运行 RedBlackTree 的性能测试
            BenchmarkRunner.Run<RedBlackTreePerformanceTests>();

            // 运行 SortedSet 的性能测试
            BenchmarkRunner.Run<SortedSetPerformanceTests>();
        }
    }

    public class ConfigWithMedian : ManualConfig
    {
        public ConfigWithMedian()
        {
            AddColumn(StatisticColumn.Median); // 添加 Median 列
        }
    }
}