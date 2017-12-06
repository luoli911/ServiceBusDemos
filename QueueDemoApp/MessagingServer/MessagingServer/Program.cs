using QueueDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace MessagingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueConnector.Initialize();
            var order = new OnlineOrder() { Customer = "alice", Product = "notebook" };
            var message = new BrokeredMessage(order);
           
            QueueConnector.OrdersQueueClient.Send(message);
            Console.ReadLine();
        }
    }
}
