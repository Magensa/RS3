using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RS3SampleCode.Service;
using RS3SampleCode.UIHandler;
using System;

namespace RS3SampleCode.App
{
    class Program
    {
        static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            IServiceCollection services = new ServiceCollection();
            CredentialModel credential = new CredentialModel();
            config.GetSection("Credential").Bind(credential);
            if (string.IsNullOrWhiteSpace(credential.CustomerCode)
                || string.IsNullOrWhiteSpace(credential.Username)
                || string.IsNullOrWhiteSpace(credential.Password)
                )
            {
                Console.WriteLine("Please check CustomerCode, Username and Password in appsettings.json");
                Console.ReadLine();
                Environment.Exit(0);
            }
            services.AddSingleton(credential);
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<IUIFactory, UIFactory>();
            services.AddSingleton<IRS3Client, RS3Client>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var uiFactory = serviceProvider.GetService<IUIFactory>();
            while (true)
            {
                try
                {
                    Console.WriteLine("Please Select an option or service operation");
                    Console.WriteLine("Enter Option number (1: Generate EMVConfig Token, ");
                    Console.WriteLine("                     2: Transform EMVConfig,");
                    Console.WriteLine("                     3: Retrieve list of keys,");
                    Console.WriteLine("                     4: Generate a Token for initial DUKPT TR31 Key Block)");
                    var keyInfo = Console.ReadKey();
                    Console.WriteLine();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.D1:
                            uiFactory.ShowUI(UI.EMVCONFIG_TOKEN);
                            break;
                        case ConsoleKey.D2:
                            uiFactory.ShowUI(UI.EMVCONFIG_TRANSFORM);
                            break;
                        case ConsoleKey.D3:
                            uiFactory.ShowUI(UI.KEY_RETRIEVE);
                            break;
                        case ConsoleKey.D4:
                            uiFactory.ShowUI(UI.KEY_TOKEN);
                            break;
                    }
                    bool decision = Confirm("Would you like to Continue with other Request?");
                    if (decision)
                        continue;
                    else
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static bool Confirm(string title)
        {
            ConsoleKey response;
            do
            {
                Console.Write($"{ title } [y/n] ");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return (response == ConsoleKey.Y);
        }
    }
}
