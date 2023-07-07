using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static async void StartContinuationTasks()
        {
            var parentTask = Task.Run(() =>
            {
                Console.WriteLine("Parent Task: Started");
                Thread.Sleep(2000);  // Simulating some work
                Console.WriteLine("Parent Task: Finished");
                return true;  // Success
            });

            // Continuation Task A (executed regardless of parent task result)
            var continuationTaskA = parentTask.ContinueWith(t =>
            {
                Console.WriteLine("Continuation Task A: Executing");
                // Perform continuation task A logic here
            });

            // Continuation Task B (executed when parent task finished without success)
            var continuationTaskB = parentTask.ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion && t.Result == true)
                    return;

                Console.WriteLine("Continuation Task B: Executing");
                // Perform continuation task B logic here
            });

            // Continuation Task C (executed when parent task would be finished with fail and parent task thread should be reused for continuation)
            var continuationTaskC = parentTask.ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion && t.Result == true)
                    return;

                Console.WriteLine("Continuation Task C: Executing");
                // Perform continuation task C logic here
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            // Continuation Task D (executed outside of the thread pool when parent task would be cancelled)
            var continuationTaskD = parentTask.ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion && t.Result == true)
                    return;

                Console.WriteLine("Continuation Task D: Executing");
                // Perform continuation task D logic here
            }, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);

            await Task.WhenAll(continuationTaskA, continuationTaskB, continuationTaskC, continuationTaskD);

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code
            StartContinuationTasks();

            Console.ReadLine();
        }
    }
}
