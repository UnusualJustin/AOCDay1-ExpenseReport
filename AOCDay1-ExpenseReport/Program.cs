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

            List<int> addends = FindAddends(2020, values, 2);
            Console.WriteLine("{0} = 2020; {1} = {2}", 
                              string.Join(" + ", addends.Select(a => a.ToString())),
                              string.Join(" * ", addends.Select(a => a.ToString())),
                              CalculateProduct(addends));

            List<int> addends2 = FindAddends(2020, values, 3);
            Console.WriteLine("{0} = 2020; {1} = {2}", 
                              string.Join(" + ", addends2.Select(a => a.ToString())),
                              string.Join(" * ", addends2.Select(a => a.ToString())),
                              CalculateProduct(addends2));
        }

        static List<int> FindAddends(int sum, int[] values, int numberOfAddends)
        {
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
                    List<int> appends = FindAddends(sum - values[i], values.Skip(i).ToArray(), numberOfAddends - 1);
                    if (appends != null)
                    {
                        appends.Insert(0, values[i]);
                        return appends;
                    }
                }
            }

            return null;
        }

        static int CalculateProduct(List<int> factors)
        { 
            return factors.Aggregate(1, (x, y) => x * y);
        }
    }
}