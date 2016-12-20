using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

using Grpc.Core;

using Helloworld;

using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace StatelessSandbox {
  public class GRpcListener : ICommunicationListener
  {
    private readonly StatelessServiceContext _ctx;

    private readonly int _port;

    private readonly Server _server;

    private readonly string _ipAddress;

    public GRpcListener( StatelessServiceContext ctx, int port ) {
      _ctx = ctx;
      _port = port;
      _ipAddress = FabricRuntime.GetNodeContext().IPAddressOrFQDN;
      _server = new Server {
        Services = { Greeter.BindService( new GreeterImpl() ) },
        Ports = { new ServerPort( _ipAddress, port, ServerCredentials.Insecure ) }
      };
    }

    public Task<string> OpenAsync( CancellationToken cancellationToken ) {
      return Task.Run( () => {
        _server.Start();
        return $"tcp::{_ipAddress}:{_port}";
      }, cancellationToken );
    }

    public Task CloseAsync( CancellationToken cancellationToken ) {
      return _server.ShutdownAsync();
    }

    public void Abort() {
      _server.KillAsync().RunSynchronously();
    }
  }
}