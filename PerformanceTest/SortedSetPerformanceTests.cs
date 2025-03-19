using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace PerformanceTest
{
    public class SortedSetPerformanceTests
    {
        private SortedSet<int> sortedSet;

        [GlobalSetup]
        public void Setup()
        {
            sortedSet = new SortedSet<int>();
        }

        [Benchmark]
        public void InsertTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                sortedSet.Add(i);
            }
        }

        [Benchmark]
        public void DeleteTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                sortedSet.Add(i);
            }

            for (int i = 0; i < 10000; i++)
            {
                sortedSet.Remove(i);
            }
        }

        [Benchmark]
        public void SearchTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                sortedSet.Add(i);
            }

            for (int i = 0; i < 10000; i++)
            {
                sortedSet.Contains(i);
            }
        }
    }
}