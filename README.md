# ServiceBusDemos
Service Bus based on cloud, message-oriented middleware technologies.
Inculding reliable message queuing,durable publish/subscribe messaging. 

Queue
offer FIFO message delivery to one or more computing consumers. Each message is received and processed by only one message consumer.
Service Bus use Queue : https://docs.microsoft.com/zh-cn/azure/service-bus-messaging/service-bus-dotnet-multi-tier-app-using-service-bus-queues#create-the-worker-role

Topics and subscriptions 
In contrast to queues, in which each message is processed by a single customer, topics and subscriptions provide one-to-many form of communication. Messages are sent to a topic, and received from subscriptions.
https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions#5-receive-messages-from-the-subscription

another messaging use Relays:
Service Bus use Relays : https://docs.microsoft.com/zh-cn/azure/service-bus-relay/service-bus-dotnet-hybrid-app-using-service-bus-relay
