using Grpc.Core;
using GrpcService.Core;
using System;
using System.IO;

namespace GrpcClient.Framework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Support https.
            SslCredentials secureCredentials = new SslCredentials(File.ReadAllText("test.cer"));
            var channel = new Channel("localhost:5001", secureCredentials);
            #endregion
            //var channel = new Channel("127.0.0.1:5000", ChannelCredentials.Insecure);
            var client = new MyService.MyServiceClient(channel);
            var person = client.SendMessage(new SearchPerson
            {
                Id = 1,
                Married = true,
                Name = "Kael Tian",
                Salary = 10086
            });
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
