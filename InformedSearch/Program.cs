using System;
using InformedSearch.Logic;

namespace InformedSearch
{
    internal class Program
    {
        internal static void Main(string[] args)
        { 
           Console.ForegroundColor = ConsoleColor.Green;

           const int smallBucketCapacity = 5;
           const int bigBucketCapacity = 9;
           const int goalVolume = 3;

           Console.WriteLine($"Capacity of a small bucket: {smallBucketCapacity} liters");
           Console.WriteLine($"Capacity of a big bucket: {bigBucketCapacity} liters");
           Console.WriteLine($"Goal volume to measure: {goalVolume} liters");
           Console.WriteLine("Looking for solution using RBFS algorithm...");

           Problem problem = new Problem(smallBucketCapacity, bigBucketCapacity, goalVolume);
           RecursiveBestFirstSearch rbfs = new RecursiveBestFirstSearch(new SimpleHeuristicFunction(problem));
           Node node = rbfs.ExecuteSearch(problem);

           if (node != null)
           {
               Console.WriteLine();
               Console.WriteLine($"Solution ({node.GetLevel()} steps):");
               Console.WriteLine(node.GetRenderedPathFromRoot());
           } else
           {
               Console.WriteLine("Solution does not exist.");
           }
           
           Console.ReadLine();
        }
    }
}