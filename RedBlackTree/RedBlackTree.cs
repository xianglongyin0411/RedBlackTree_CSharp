using System;
using System.Collections.Generic;

// 定义颜色枚举
public enum Color
{
    Red,
    Black
}

// 定义红黑树节点类
public class Node<T> where T : IComparable<T>
{
    public T Key { get; set; }
    public Node<T> Left { get; set; }
    public Node<T> Right { get; set; }
    public Node<T> Parent { get; set; }
    public Color Color { get; set; }

    public Node(T key)
    {
        Key = key;
        Color = Color.Red;
    }
}

// 定义红黑树类
public class RedBlackTree<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get { return root; }
    }

    // 左旋操作
    public void LeftRotate(Node<T> x)
    {
        if (x == null) return;
        Node<T> y = x.Right;
        x.Right = y.Left;
        if (y.Left != null)
        {
            y.Left.Parent = x;
        }
        y.Parent = x.Parent;
        if (x.Parent == null)
        {
            root = y;
        }
        else if (x == x.Parent.Left)
        {
            x.Parent.Left = y;
        }
        else
        {
            x.Parent.Right = y;
        }
        y.Left = x;
        x.Parent = y;
    }

    // 右旋操作
    public void RightRotate(Node<T> x)
    {
        if (x == null) return;
        Node<T> y = x.Left;
        x.Left = y.Right;
        if (y.Right != null)
        {
            y.Right.Parent = x;
        }
        y.Parent = x.Parent;
        if (x.Parent == null)
        {
            root = y;
        }
        else if (x == x.Parent.Right)
        {
            x.Parent.Right = y;
        }
        else
        {
            x.Parent.Left = y;
        }
        y.Right = x;
        x.Parent = y;
    }

    // 插入修复操作
    private void InsertFixup(Node<T> z)
    {
        while (z.Parent != null && z.Parent.Color == Color.Red)
        {
            if (z.Parent == z.Parent.Parent.Left)
            {
                Node<T> y = z.Parent.Parent.Right; // uncle节点
                if (y != null && y.Color == Color.Red) // parent节点红，uncle节点红
                {
                    z.Parent.Color = Color.Black;
                    y.Color = Color.Black;
                    z.Parent.Parent.Color = Color.Red;
                    z = z.Parent.Parent; // 向上递归
                }
                else // parent节点红，uncle节点null或者黑
                {
                    if (z == z.Parent.Right) // LR
                    {
                        z = z.Parent;
                        LeftRotate(z);
                    }
                    z.Parent.Color = Color.Black;
                    z.Parent.Parent.Color = Color.Red;
                    RightRotate(z.Parent.Parent);
                }
            }
            else
            {
                Node<T> y = z.Parent.Parent.Left; // uncle节点
                if (y != null && y.Color == Color.Red) // parent节点红，uncle节点红
                {
                    z.Parent.Color = Color.Black;
                    y.Color = Color.Black;
                    z.Parent.Parent.Color = Color.Red;
                    z = z.Parent.Parent; // 向上递归
                }
                else // parent节点红，uncle节点null或者黑
                {
                    if (z == z.Parent.Left) // RL
                    {
                        z = z.Parent;
                        RightRotate(z);
                    }
                    z.Parent.Color = Color.Black;
                    z.Parent.Parent.Color = Color.Red;
                    LeftRotate(z.Parent.Parent);
                }
            }
        }
        root.Color = Color.Black;
    }

    // 插入操作
    public void Insert(T key)
    {
        Node<T> z = new Node<T>(key);
        Node<T> y = null;
        Node<T> x = root;

        // 查找插入位置
        while (x != null)
        {
            y = x;
            int cmp = z.Key.CompareTo(x.Key);
            if (cmp == 0)
            {
                // 如果键已存在，直接返回
                return;
            }
            else if (cmp < 0)
            {
                x = x.Left;
            }
            else
            {
                x = x.Right;
            }
        }

        z.Parent = y;
        if (y == null)
        {
            root = z; // 树为空时，z 为根节点
        }
        else if (z.Key.CompareTo(y.Key) < 0)
        {
            y.Left = z;
        }
        else
        {
            y.Right = z;
        }

        InsertFixup(z); // 插入修复
    }

    // 查找最小节点
    private Node<T> Minimum(Node<T> x)
    {
        if (x == null)
        {
            return null;
        }
        while (x.Left != null)
        {
            x = x.Left;
        }
        return x;
    }

    // 替换节点
    private void Transplant(Node<T> u, Node<T> v)
    {
        if (u.Parent == null)
        {
            root = v;
        }
        else if (u == u.Parent.Left)
        {
            u.Parent.Left = v;
        }
        else
        {
            u.Parent.Right = v;
        }
        if (v != null)
        {
            v.Parent = u.Parent;
        }
    }

    // 查找操作
    public Node<T> Search(T key)
    {
        Node<T> x = root;
        while (x != null)
        {
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
            {
                x = x.Left;
            }
            else if (cmp > 0)
            {
                x = x.Right;
            }
            else
            {
                return x;
            }
        }
        return null;
    }

    // 删除修复操作
    private void DeleteFixup(Node<T> x)
    {
        while (x != root && (x == null || x.Color == Color.Black))
        {
            if (x == x.Parent.Left)
            {
                Node<T> w = x.Parent.Right;
                if (w != null && w.Color == Color.Red)
                {
                    w.Color = Color.Black;
                    x.Parent.Color = Color.Red;
                    LeftRotate(x.Parent);
                    w = x.Parent.Right;
                }

                bool leftIsBlack = (w == null || w.Left == null || w.Left.Color == Color.Black);
                bool rightIsBlack = (w == null || w.Right == null || w.Right.Color == Color.Black);

                if (leftIsBlack && rightIsBlack)
                {
                    if (w != null)
                    {
                        w.Color = Color.Red;
                    }
                    x = x.Parent;
                }
                else
                {
                    if (w != null && (w.Right == null || w.Right.Color == Color.Black))
                    {
                        if (w.Left != null)
                        {
                            w.Left.Color = Color.Black;
                        }
                        w.Color = Color.Red;
                        RightRotate(w);
                        w = x.Parent.Right;
                    }

                    if (w != null)
                    {
                        w.Color = x.Parent.Color;
                        x.Parent.Color = Color.Black;
                        if (w.Right != null)
                        {
                            w.Right.Color = Color.Black;
                        }
                    }
                    LeftRotate(x.Parent);
                    x = root;
                }
            }
            else
            {
                Node<T> w = x.Parent.Left;
                if (w != null && w.Color == Color.Red)
                {
                    w.Color = Color.Black;
                    x.Parent.Color = Color.Red;
                    RightRotate(x.Parent);
                    w = x.Parent.Left;
                }

                bool rightIsBlack = (w == null || w.Right == null || w.Right.Color == Color.Black);
                bool leftIsBlack = (w == null || w.Left == null || w.Left.Color == Color.Black);

                if (rightIsBlack && leftIsBlack)
                {
                    if (w != null)
                    {
                        w.Color = Color.Red;
                    }
                    x = x.Parent;
                }
                else
                {
                    if (w != null && (w.Left == null || w.Left.Color == Color.Black))
                    {
                        if (w.Right != null)
                        {
                            w.Right.Color = Color.Black;
                        }
                        w.Color = Color.Red;
                        LeftRotate(w);
                        w = x.Parent.Left;
                    }

                    if (w != null)
                    {
                        w.Color = x.Parent.Color;
                        x.Parent.Color = Color.Black;
                        if (w.Left != null)
                        {
                            w.Left.Color = Color.Black;
                        }
                    }
                    RightRotate(x.Parent);
                    x = root;
                }
            }
        }
        if (x != null)
        {
            x.Color = Color.Black;
        }
    }

    // 删除操作
    public void Delete(T key)
    {
        Node<T> z = Search(key);
        if (z == null) return;

        Node<T> y = z;
        Color yOriginalColor = y.Color;
        Node<T> x;

        if (z.Left == null)
        {
            x = z.Right;
            Transplant(z, z.Right);
        }
        else if (z.Right == null)
        {
            x = z.Left;
            Transplant(z, z.Left);
        }
        else
        {
            y = Minimum(z.Right);
            yOriginalColor = y.Color;
            x = y.Right;
            if (y.Parent == z)
            {
                if (x != null) x.Parent = y;
            }
            else
            {
                Transplant(y, y.Right);
                y.Right = z.Right;
                y.Right.Parent = y;
            }
            Transplant(z, y);
            y.Left = z.Left;
            y.Left.Parent = y;
            y.Color = z.Color;
        }

        if (yOriginalColor == Color.Black)
        {
            if (x == null)
            {
                x = new Node<T>(default(T)) { Parent = y.Parent, Color = Color.Black };
                if (y.Parent != null)
                {
                    if (y == y.Parent.Left)
                    {
                        y.Parent.Left = x;
                    }
                    else
                    {
                        y.Parent.Right = x;
                    }
                }
                else
                {
                    root = x;
                }
            }
            DeleteFixup(x);

            if (x != null && x.Key.Equals(default(T)))
            {
                if (x.Parent != null)
                {
                    if (x.Parent.Left == x)
                    {
                        x.Parent.Left = null;
                    }
                    else if (x.Parent.Right == x)
                    {
                        x.Parent.Right = null;
                    }
                }
            }
        }
    }

    public List<T> InOrderTraversal()
    {
        var result = new List<T>();
        InOrderTraversal(root, result);
        return result;
    }

    private void InOrderTraversal(Node<T> node, List<T> result)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, result);
            result.Add(node.Key);
            InOrderTraversal(node.Right, result);
        }
    }
}