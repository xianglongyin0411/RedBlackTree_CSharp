using Xunit;
using System;
using System.Collections.Generic;

namespace RedBlackTreeApp.Tests
{
    public class RedBlackTreeTests
    {
        [Fact]
        public void LeftRotate_UpdatesNodeRelationshipsCorrectly()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            Node<int> x = tree.Root;
            Node<int> y = x.Right;

            // 执行
            tree.LeftRotate(x);

            // 断言
            Assert.Equal(y, tree.Root);
            Assert.Equal(x, y.Left);
            Assert.Null(x.Right);
            Assert.Equal(y, x.Parent);
        }

        [Fact]
        public void RightRotate_UpdatesNodeRelationshipsCorrectly()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Insert(2);
            tree.Insert(1);
            Node<int> x = tree.Root;
            Node<int> y = x.Left;

            // 执行
            tree.RightRotate(x);

            // 断言
            Assert.Equal(y, tree.Root);
            Assert.Equal(x, y.Right);
            Assert.Null(x.Left);
            Assert.Equal(y, x.Parent);
        }

        [Fact]
        public void InsertFixup_MaintainsRedBlackProperties()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] keys = { 10, 5, 18, 8, 14, 27, 13, 3, 15, 25, 2, 26, 20 };
            foreach (int key in keys)
            {
                tree.Insert(key);
            }
            Node<int> root = tree.Root;

            // 断言
            Assert.Equal(Color.Black, root.Color);
            AssertNoConsecutiveRedNodes(root);

            int blackHeight = CalculateBlackHeight(root);
            Assert.True(VerifyBlackHeight(root, blackHeight));
        }

        [Fact]
        public void Delete_NodeRemovalAndPropertiesMaintained()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] keys = { 10, 20, 30, 15, 25, 5, 35 };
            foreach (int key in keys)
            {
                tree.Insert(key);
            }

            // 删除节点
            tree.Delete(20);
            Assert.Null(tree.Search(20));

            tree.Delete(5);
            Assert.Null(tree.Search(5));

            // 断言红黑树性质
            Node<int> root = tree.Root;
            Assert.Equal(Color.Black, root.Color);
            AssertNoConsecutiveRedNodes(root);

            int blackHeight = CalculateBlackHeight(root);
            Assert.True(VerifyBlackHeight(root, blackHeight));
        }

        [Fact]
        public void Search_ExistingKey_ReturnsNode()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] keys = { 10, 20, 30, 15, 25 };
            foreach (int key in keys)
            {
                tree.Insert(key);
            }

            // 执行
            Node<int> result = tree.Search(20);

            // 断言
            Assert.NotNull(result);
            Assert.Equal(20, result.Key);
        }

        [Fact]
        public void Search_NonExistingKey_ReturnsNull()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] keys = { 10, 20, 30 };
            foreach (int key in keys)
            {
                tree.Insert(key);
            }

            // 执行
            Node<int> result = tree.Search(40);

            // 断言
            Assert.Null(result);
        }

        [Fact]
        public void InOrderTraversal_ReturnsSortedKeys()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] keys = { 10, 20, 30, 15, 25, 5, 35 };
            foreach (int key in keys)
            {
                tree.Insert(key);
            }

            // 执行
            List<int> result = tree.InOrderTraversal();

            // 断言
            Assert.Equal(new List<int> { 5, 10, 15, 20, 25, 30, 35 }, result);
        }

        [Fact]
        public void Insert_DuplicateKey_DoesNotChangeTree()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Insert(10);
            tree.Insert(20);

            // 插入重复键
            tree.Insert(10);

            // 执行
            List<int> result = tree.InOrderTraversal();

            // 断言
            Assert.Equal(new List<int> { 10, 20 }, result);
        }

        [Fact]
        public void Delete_RootNode_UpdatesTreeCorrectly()
        {
            // 准备
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] keys = { 10, 20, 30, 15, 25, 5, 35 };
            foreach (int key in keys)
            {
                tree.Insert(key);
            }

            // 删除根节点
            tree.Delete(10);

            // 断言
            Assert.Null(tree.Search(10));
            Assert.Equal(Color.Black, tree.Root.Color);
        }

        // 辅助方法：验证没有连续的红节点
        private void AssertNoConsecutiveRedNodes(Node<int> node)
        {
            if (node == null) return;
            if (node.Color == Color.Red)
            {
                if (node.Left != null && node.Left.Color == Color.Red)
                {
                    Assert.Fail("存在连续的红节点");
                }
                if (node.Right != null && node.Right.Color == Color.Red)
                {
                    Assert.Fail("存在连续的红节点");
                }
            }
            AssertNoConsecutiveRedNodes(node.Left);
            AssertNoConsecutiveRedNodes(node.Right);
        }

        // 辅助方法：计算黑色节点高度
        private int CalculateBlackHeight(Node<int> node)
        {
            if (node == null) return 1;
            int leftBlackHeight = CalculateBlackHeight(node.Left);
            int rightBlackHeight = CalculateBlackHeight(node.Right);

            if (leftBlackHeight != rightBlackHeight)
            {
                Assert.Fail("从节点到叶子节点的黑色节点数不同");
            }

            return node.Color == Color.Black ? leftBlackHeight + 1 : leftBlackHeight;
        }

        // 辅助方法：验证黑色节点高度是否一致
        private bool VerifyBlackHeight(Node<int> node, int expectedBlackHeight)
        {
            if (node == null) return expectedBlackHeight == 1;
            int currentBlackHeight = node.Color == Color.Black ? expectedBlackHeight - 1 : expectedBlackHeight;
            return VerifyBlackHeight(node.Left, currentBlackHeight) && VerifyBlackHeight(node.Right, currentBlackHeight);
        }
    }
}