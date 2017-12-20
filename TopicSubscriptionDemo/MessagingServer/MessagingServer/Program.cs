using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using Microsoft.ServiceBus.Messaging;

namespace MessagingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var orders = new List<OnlineOrder>()
            {
                new OnlineOrder { Customer = "alice", Product = "notebook" },
                new OnlineOrder { Customer = "alice", Product = "computer" },
                new OnlineOrder { Customer = "mary", Product = "jewery" }

            };
            List<BrokeredMessage> messageList = new List<BrokeredMessage>();
            for(var i = 0; i < orders.Count; i++)
            {
                //serializer object to message
                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(OnlineOrder));
                ser.WriteObject(stream, orders[i]);
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                string json = sr.ReadToEnd();
                var message = new BrokeredMessage(json);
                //enable message to send json formats data
                message.ContentType = "application/json";
                messageList.Add(message);
            }

            TopicConnector.Initialize();
            var namespaceManager = TopicConnector.namespaceManager;
            //two subscriptions to receive a topic
            var myAgentSubscription = TopicConnector.myAgentSubscription;
            var myAuditSubscription = TopicConnector.myAuditSubscription;

            var myTopicClient = TopicConnector.messagingFactory.CreateTopicClient(TopicConnector.TopicName);

            foreach(BrokeredMessage message in messageList)
            {
                myTopicClient.Send(message);
                Thread.Sleep(10000);
                Console.WriteLine(string.Format("Message sent: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
            }
            
            Console.ReadLine();
        }
    }
}
