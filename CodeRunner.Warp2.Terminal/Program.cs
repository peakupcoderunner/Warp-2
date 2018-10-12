using Amazon.IdentityManagement;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeRunner.Warp2.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var options = config.GetAWSOptions();
            var client = options.CreateServiceClient<IAmazonIdentityManagementService>() as AmazonIdentityManagementServiceClient;
            var response = await client.GetUserAsync();
            var user = response.User;
            Console.WriteLine("Hello {0}! The user was created on {1}", user.UserName, user.CreateDate);
            Console.ReadKey(true);
        }
    }
}
