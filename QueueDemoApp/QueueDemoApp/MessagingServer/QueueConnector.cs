using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace MessagingServer
{
    public static class QueueConnector
    {
        public static QueueClient OrdersQueueClient;
        public const string Namespace = "sbnstest";
        public const string QueueName = "OrdersQueue";
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
            var namespaceManager = CreateNamespaceManager();
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }
            var messagingFactory = MessagingFactory.Create(
                namespaceManager.Address,
                namespaceManager.Settings.TokenProvider);
            OrdersQueueClient = messagingFactory.CreateQueueClient("OrdersQueue");
        }
    }
}