using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var greeterClient = new GrpcServer.Greeter.GreeterClient(channel);
                Console.WriteLine("Please enter a number value.");
                var serviceResult = greeterClient.GetMultiplacationResult(new GrpcServer.MultiplicationRequest { Number = int.Parse(Console.ReadLine()) });

                await Task.Run(async () =>
                {
                    while (await serviceResult.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                        Console.WriteLine($"{serviceResult.ResponseStream.Current.Message}");
                });
            }
        }
    }
}
