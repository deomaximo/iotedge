// Copyright (c) Microsoft. All rights reserved.
namespace Microsoft.Azure.Devices.Edge.Hub.MqttBrokerAdapter
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices.Edge.Hub.Core;
    using Microsoft.Azure.Devices.Edge.Hub.Core.Cloud;
    using Microsoft.Azure.Devices.Edge.Hub.Core.Identity;
    using Microsoft.Azure.Devices.Edge.Util;
    using Microsoft.Azure.Devices.Edge.Util.TransientFaultHandling;

    public class BrokeredCloudConnectionProvider : ICloudConnectionProvider
    {
        BrokeredCloudProxyDispatcher cloudProxyDispatcher;

        public BrokeredCloudConnectionProvider(BrokeredCloudProxyDispatcher cloudProxyDispatcher)
        {
            this.cloudProxyDispatcher = cloudProxyDispatcher;
        }

        public void BindEdgeHub(IEdgeHub edgeHub)
        {
            this.cloudProxyDispatcher.BindEdgeHub(edgeHub);
        }

        public Task<Try<ICloudConnection>> Connect(IClientCredentials clientCredentials, Action<string, CloudConnectionStatus> connectionStatusChangedHandler)
        {
            return this.Connect(clientCredentials.Identity, connectionStatusChangedHandler);
        }

        public async Task<Try<ICloudConnection>> Connect(IIdentity identity, Action<string, CloudConnectionStatus> connectionStatusChangedHandler)
        {
            if (!await this.IsConnected())
            {
                return new Try<ICloudConnection>(new Exception("Bridge is not connected upstream"));
            }

            var cloudProxy = new BrokeredCloudProxy(identity, this.cloudProxyDispatcher, connectionStatusChangedHandler);
            return new Try<ICloudConnection>(new BrokeredCloudConnection(cloudProxy));
        }

        // The purpose of this method is to make less noise in logs when the broker
        // is not connected upstream for a short interim time period.
        // This mainly happens during startup, when edgeHub core starts connecting upstream,
        // but some collaborating components are not ready yet, or the parent device is not
        // available yet.
        async Task<bool> IsConnected()
        {
            var shouldRetry = new FixedInterval(50, TimeSpan.FromMilliseconds(100)).GetShouldRetry();
            var retryCount = 0;

            while (!this.cloudProxyDispatcher.IsConnected && shouldRetry(retryCount++, null, out var delay))
            {
                await Task.Delay(delay);
            }

            return this.cloudProxyDispatcher.IsConnected;
        }
    }
}
