using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace MessagingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            
            TopicConnector.Initialize();
            var messagingFactory = TopicConnector.messagingFactory;
            //create two subscription clients to receive topic massage
            SubscriptionClient agentSubscriptionClient = messagingFactory.CreateSubscriptionClient(TopicConnector.TopicName, "Inventory", ReceiveMode.PeekLock);
            SubscriptionClient auditSubscriptionClient = messagingFactory.CreateSubscriptionClient(TopicConnector.TopicName, "Dashboard", ReceiveMode.ReceiveAndDelete);

            var message1 = new BrokeredMessage();
            
            while ((message1 = agentSubscriptionClient.Receive())!= null)
            {
                //deserialize Json data
                message1.ContentType = "application/json";
                var ms = new MemoryStream(Encoding.Unicode.GetBytes(message1.GetBody<string>()));
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(OnlineOrder));
                ms.Position = 0;
                OnlineOrder order = (OnlineOrder)deserializer.ReadObject(ms);
                Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1} {2}",message1.MessageId ,order.Customer,order.Product));
                message1.Complete();
            }
            var message2 = new BrokeredMessage();
            while ((message2 = auditSubscriptionClient.Receive())!= null)
            {
                message2.ContentType = "application/json";
                var ms = new MemoryStream(Encoding.Unicode.GetBytes(message2.GetBody<string>()));
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(OnlineOrder));
                ms.Position = 0;
                OnlineOrder order = (OnlineOrder)deserializer.ReadObject(ms);
                Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1} {2}", message2.MessageId, order.Customer, order.Product));
            }
            Console.ReadLine();
        }
    }
}
