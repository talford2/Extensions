using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dataway.Extensions
{
    public static class ListExtensions
    {
        public static void MoveUp<T>(this List<T> list, int index)
        {
            if (index > 0)
            {
                var oldItem = list[index];
                list[index] = list[index - 1];
                list[index - 1] = oldItem;
            }
        }

        public static void MoveUp<T>(this List<T> list, Predicate<T> predicate)
        {
            MoveUp<T>(list, list.FindIndex(predicate));
        }

        public static void MoveDown<T>(this List<T> list, int index)
        {
            if ((index + 1) < list.Count)
            {
                var oldItem = list[index];
                list[index] = list[index + 1];
                list[index + 1] = oldItem;
            }
        }

        public static void MoveDown<T>(this List<T> list, Predicate<T> predicate)
        {
            MoveDown<T>(list, list.FindIndex(predicate));
        }

        public static List<List<T>> DivideIntoNLists<T>(this List<T> list, int n, DivideDirection direction)
        {
            List<List<T>> columns = new List<List<T>>();
            for (int i = 0; i < n; i++)
            {
                columns.Add(new List<T>());
            }

            if (direction == DivideDirection.Horizontal)
            {
                int col = 0;
                foreach (var item in list)
                {
                    columns[col].Add(item);
                    col++;
                    if (col >= n)
                    {
                        col = 0;
                    }
                }
            }
            else
            {
                var colCount = list.Count / (float)n;

                int index = 0;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < colCount && index < list.Count; j++)
                    {
                        columns[i].Add(list[index]);
                        index++;
                    }
                }
            }

            return columns;
        }

        public static List<Dictionary<T, S>> DivideIntoNList<T, S>(this Dictionary<T, S> dictionary, int n, DivideDirection direction)
        {
            List<Dictionary<T, S>> columns = new List<Dictionary<T, S>>();

            for (int i = 0; i < n; i++)
            {
                columns.Add(new Dictionary<T, S>());
            }

            if (direction == DivideDirection.Horizontal)
            {
                int col = 0;
                foreach (var item in dictionary)
                {
                    columns[col].Add(item.Key, item.Value);
                    col++;
                    if (col >= n)
                    {
                        col = 0;
                    }
                }
            }
            else
            {
                var colCount = dictionary.Count / (float)n;

                int index = 0;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < colCount && index < dictionary.Count; j++)
                    {
                        var hhh = dictionary.ElementAt(index);
                        columns[i].Add(hhh.Key, hhh.Value);
                        index++;
                    }
                }
            }

            return columns;
        }

        public enum DivideDirection
        {
            Horizontal,
            Vertical
        }
    }
}