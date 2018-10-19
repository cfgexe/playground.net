using System;

namespace ConsolePlayground
{
    class Program
    {
        private static void Main(string[] args)
        {
            DataProtection.DataProtectionDemo.Test();
            DataProtection.DataProtectionDemo.TestEncryptedKeys();

            WaitForExit();
        }

        private static void WaitForExit()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
