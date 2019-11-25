using System;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    class Program
    {
        async static  Task Main(string[] args)
        {
            var p = new Program();
            Console.WriteLine("Hello World!");
            try
            {
                var sum = await p.Run();
                Console.WriteLine($"sum ={sum}");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task<long> Run()
        {
            TaskCompletionSource<long> tcs = new System.Threading.Tasks.TaskCompletionSource<long>();
            
            var asncTask = Task.Run(() => Test());
            asncTask.ContinueWith((a) => tcs.SetResult(a.Result));

            var result = await Task.WhenAny(tcs.Task, Task.Delay(1000));
            if (result.Id == tcs.Task.Id)
            {
                return await tcs.Task;
            }
            else
                throw new TimeoutException();
           
        }

        private long Test()
        {
            long sum = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
