using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOCDay2_ExpenseReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            int[] values = lines.Select(l => int.Parse(l))
                                .OrderBy(v => v)
                                .ToArray();

            FindAddendsAndMultiply(values, 2020, 2);
            FindAddendsAndMultiply(values, 2020, 3);
            FindAddendsAndMultiply(values, 3852, 4);
            FindAddendsAndMultiply(values, 5145, 5);
            FindAddendsAndMultiply(values, 6517, 6);
            FindAddendsAndMultiply(values, 7400, 7);
            FindAddendsAndMultiply(values, 10000, 8);
        }

        static void FindAddendsAndMultiply(int[] values, int sum, int numberOfAddends)
        {
            List<int> addends = FindAddends(sum, values, numberOfAddends);
            if (addends != null)
            {
                Console.WriteLine("{0} = 2020; {1} = {2}",
                              string.Join(" + ", addends.Select(a => a.ToString())),
                              string.Join(" * ", addends.Select(a => a.ToString())),
                              CalculateProduct(addends));
            }
            else
            {
                Console.Error.WriteLine($"No combination of {numberOfAddends} numbers found that equals {sum}!");
            }
        }

        static List<int> FindAddends(int sum, int[] values, int numberOfAddends)
        {
            if (values.Length == 0 || values[0] * numberOfAddends > sum)
            {
                return null;
            }

            if (numberOfAddends == 2)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    for (int j = values.Length - 1; j > i; j--)
                    {
                        if (values[i] + values[j] == sum)
                        {
                            return new List<int> { values[i], values[j] };
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    List<int> appends = FindAddends(sum - values[i], values.Skip(i + 1).ToArray(), numberOfAddends - 1);
                    if (appends != null)
                    {
                        appends.Insert(0, values[i]);
                        return appends;
                    }
                }
            }

            return null;
        }

        static long CalculateProduct(List<int> factors)
        {
            long product = 1;
            foreach (int factor in factors)
            {
                product *= factor;
            }

            return product;
        }
    }
}