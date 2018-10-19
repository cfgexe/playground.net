using Microsoft.AspNetCore.DataProtection;
using System;
using System.IO;

namespace ConsolePlayground.DataProtection
{
    public static class DataProtectionDemo
    {
        private const string KeysFolderName = "_playground.net";

        public static void Test()
        {
            // Get the path to %LOCALAPPDATA%\_playground.net
            var destFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                KeysFolderName);

            // Instantiate the data protection system at this folder
            var dataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(destFolder));

            var protector = dataProtectionProvider.CreateProtector("Playground.net.No-DI");
            Console.Write("Enter input: ");
            var input = Console.ReadLine();

            // Protect the payload
            var protectedPayload = protector.Protect(input);
            Console.WriteLine($"Protect returned: {protectedPayload}");

            // Unprotect the payload
            var unprotectedPayload = protector.Unprotect(protectedPayload);
            Console.WriteLine($"Unprotect returned: {unprotectedPayload}");
        }

        public static void TestEncryptedKeys()
        {
            // Get the path to %LOCALAPPDATA%\_playground.net
            var destFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                KeysFolderName);

            // Instantiate the data protection system at this folder
            var dataProtectionProvider = DataProtectionProvider.Create(
                new DirectoryInfo(destFolder),
                configuration =>
                {
                    configuration.SetApplicationName("Playground.net 1.0");
                    configuration.ProtectKeysWithDpapi();
                });

            var protector = dataProtectionProvider.CreateProtector("Playground.net.No-DI");
            Console.Write("Enter input: ");
            var input = Console.ReadLine();

            // Protect the payload
            var protectedPayload = protector.Protect(input);
            Console.WriteLine($"Protect returned: {protectedPayload}");

            // Unprotect the payload
            var unprotectedPayload = protector.Unprotect(protectedPayload);
            Console.WriteLine($"Unprotect returned: {unprotectedPayload}");
        }
    }
}
