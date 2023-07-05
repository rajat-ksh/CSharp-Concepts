/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<int> _numbers = new ConcurrentBag<int>();
            Random random = new Random();

            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    int number = random.Next(1, 50);
                    Console.WriteLine("\nAdding Elements {0}", number);
                    _numbers.Add(number);
                    Task display = Task.Run(() =>
                    {
                        foreach (var num in _numbers)
                        {
                            Console.Write(num + " ");
                        }
                    });
                    Task.WaitAll(display);
                }
            });
            Task.WaitAll();

            Console.ReadLine();
        }
    }
}
