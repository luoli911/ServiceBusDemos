using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace MessagingServer
{
    public static class TopicConnector
    {
        public static MessagingFactory messagingFactory;
        public static SubscriptionDescription myAgentSubscription;
        public static SubscriptionDescription myAuditSubscription;
        public static NamespaceManager namespaceManager;
        public const string Namespace = "sbnstest";
        public const string TopicName = "topic1";
        public static NamespaceManager CreateNamespaceManager()
        {
            var uri = ServiceBusEnvironment.CreateServiceUri(
                "sb", Namespace, String.Empty);
            var tp = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                "RootManageSharedAccessKey", "uVQ7ALLWzzCYrA6jM+xIN+rdluchVzgw6fEuB3Z0vSI=");
            return new NamespaceManager(uri, tp);
        }
        public static void Initialize()
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;
            namespaceManager = CreateNamespaceManager();
            if (!namespaceManager.TopicExists(TopicName))
            {
                namespaceManager.CreateTopic(TopicName);
            }
            if (!namespaceManager.SubscriptionExists(TopicName, "Inventory"))
                myAgentSubscription = namespaceManager.CreateSubscription(TopicName, "Inventory");
            if (!namespaceManager.SubscriptionExists(TopicName, "Dashboard"))
                myAuditSubscription = namespaceManager.CreateSubscription(TopicName, "Dashboard");

            messagingFactory = MessagingFactory.Create(
                namespaceManager.Address,
                namespaceManager.Settings.TokenProvider);
            
        }
    }
}
