using System;
using System.Collections.Generic;

namespace CSharpPlayGrond
{
    public class BinaryTreeNode
    {
        public int Value { get; }
        public BinaryTreeNode Left { get; private set; }
        public BinaryTreeNode Right { get; private set; }

        public BinaryTreeNode(int value)
        {
            Value = value;
        }

        public BinaryTreeNode InsertLeft(int leftValue)
        {
            Left = new BinaryTreeNode(leftValue);
            return Left;
        }

        public BinaryTreeNode InsertRight(int rightValue)
        {
            Right = new BinaryTreeNode(rightValue);
            return Right;
        }
    }

    public class TreeMethods
    {
        public static int GetHeight(BinaryTreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                return (1 + Math.Max(GetHeight(root?.Left), GetHeight(root?.Right)));
            }
        }

        public static bool IsBalanced(BinaryTreeNode treeRoot)
        {
            // Determine if the tree is superbalanced
            if (treeRoot == null)
            {
                return true;
            }

            int lh = GetHeight(treeRoot?.Left);
            int rh = GetHeight(treeRoot?.Right);

            if (Math.Abs(lh - rh) <= 1)
            {
                return (IsBalanced(treeRoot?.Left) && IsBalanced(treeRoot?.Right));
            }
            else
            {
                return false;
            }
        }

        public static bool IsBinarySearchTree(BinaryTreeNode root)
        {
            // Determine if the tree is a valid binary search tree
            if (root == null)
            {
                return true;
            }

            var ischeck = true;
            if (root.Left != null)
            {
                ischeck = (root.Left.Value < root.Value);
            }
            if (ischeck && root.Right != null)
            {
                ischeck = (root.Value < root.Right.Value);
            }

            if (ischeck)
            {
                return (IsBinarySearchTree(root.Left) && IsBinarySearchTree(root.Right));
            }
            else
            {
                return false;
            }
        }

        public static void GetPreOrder(BinaryTreeNode root, List<int> preorder)
        {
            if (root == null)
            {
                return;
            }

            GetPreOrder(root.Left, preorder);
            preorder.Add(root.Value);
            GetPreOrder(root.Right, preorder);
        }
    }
}