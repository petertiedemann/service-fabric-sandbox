﻿using System.Collections.Generic;
using System.Fabric;

using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace StatelessSandbox {
  /// <summary>
  /// An instance of this class is created for each service instance by the Service Fabric runtime.
  /// </summary>
  internal sealed class StatelessSandbox: StatelessService {
    public StatelessSandbox( StatelessServiceContext context )
        : base( context ) { }

    /// <summary>
    /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
    /// </summary>
    /// <returns>A collection of listeners.</returns>
    protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners() {
      return new[] { new ServiceInstanceListener( ctx => new GRpcListener( ctx, 42000 ) ) };
    }
  }
}
