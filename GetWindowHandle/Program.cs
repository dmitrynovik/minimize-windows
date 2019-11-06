using System;

namespace GetWindowHandle
{
    class Program
    {
        static void Main(string[] args)
        {
            var winMgr = new WindowManager();
            var handles = winMgr.GetAllWindowHandles("chrome");
            foreach (var h in handles)
            {
                var result = winMgr.MinimizeWindow(h);
                Console.WriteLine("Minimizing {0} returned {1}", h, result);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
