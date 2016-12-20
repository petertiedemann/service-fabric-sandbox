using System.Threading.Tasks;

using Grpc.Core;

using Helloworld;

namespace StatelessSandbox {
  internal class GreeterImpl: Greeter.GreeterBase {
    // Server side handler of the SayHello RPC
    public override Task<HelloReply> SayHello( HelloRequest request, ServerCallContext context ) {
      return Task.FromResult( new HelloReply { Message = "Hello " + request.Name } );
    }
  }
}