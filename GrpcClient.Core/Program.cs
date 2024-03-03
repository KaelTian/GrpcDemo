// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcService.Core;
using System.Security.Cryptography.X509Certificates;

#region support https.
// 服务器地址
var serverAddress = "https://localhost:5001";

// 证书文件路径
var rootCertPath = "test.cer";

// 创建 HttpClientHandler，配置根证书
var handler = new HttpClientHandler();
handler.ClientCertificates.Add(new X509Certificate2(rootCertPath));

// 创建 HttpClient，将 HttpClientHandler 传递给 GrpcChannel
var httpClient = new HttpClient(handler);
var channel = GrpcChannel.ForAddress(serverAddress, new GrpcChannelOptions { HttpClient = httpClient });
#endregion
//using var channel = GrpcChannel.ForAddress("http://localhost:5114");
var client = new MyService.MyServiceClient(channel);
var person = await client.SendMessageAsync(new SearchPerson
{
    Id = 1,
    Married = true,
    Name = "Kael Tian",
    Salary = 10086
});

Console.WriteLine("Press any key to exit...");
Console.ReadKey();