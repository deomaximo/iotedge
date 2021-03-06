// Copyright (c) Microsoft. All rights reserved.
namespace Microsoft.Azure.Devices.Routing.Core
{
    public class NullRoutingUserMetricLogger : IRoutingUserMetricLogger
    {
        NullRoutingUserMetricLogger()
        {
        }

        public static NullRoutingUserMetricLogger Instance { get; } = new NullRoutingUserMetricLogger();

        public void LogEgressMetric(long metricValue, string iotHubName, MessageRoutingStatus messageStatus, IMessage message)
        {
        }

        public void LogEgressFallbackMetric(long metricValue, string iotHubName)
        {
        }

        public void LogEventHubEndpointEgressSuccessMetric(long metricValue, string iotHubName)
        {
        }

        public void LogEventHubEndpointLatencyMetric(long metricValue, string iotHubName)
        {
        }

        public void LogQueueEndpointEgressSuccessMetric(long metricValue, string iotHubName)
        {
        }

        public void LogQueueEndpointLatencyMetric(long metricValue, string iotHubName)
        {
        }

        public void LogTopicEndpointEgressSuccessMetric(long metricValue, string iotHubName)
        {
        }

        public void LogTopicEndpointLatencyMetric(long metricValue, string iotHubName)
        {
        }

        public void LogBuiltInEndpointEgressSuccessMetric(long metricValue, string iotHubName)
        {
        }

        public void LogBuiltInEndpointLatencyMetric(long metricValue, string iotHubName)
        {
        }

        public void LogIngressFailureMetric(long metricValue, string iothubName, IMessage message, string reason)
        {
        }

        public void LogRetryOperation(long metricValue, string iothubName, string id, string type)
        {
        }
    }
}
