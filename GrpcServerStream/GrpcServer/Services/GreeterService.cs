using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task GetMultiplacationResult(MultiplicationRequest request, IServerStreamWriter<ServiceResult> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Incoming Value : {request.Number}");

            await Task.Run(async () =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    await responseStream.WriteAsync(new ServiceResult { Message = $"{request.Number}*{i}={request.Number * i}" });
                    await Task.Delay(500);
                }
            });
        }
    }
}
