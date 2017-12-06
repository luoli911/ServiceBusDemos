﻿using Microsoft.ServiceBus.Messaging;
using QueueDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueueDemoApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var namespaceManager = QueueConnector.CreateNamespaceManager();
            var queue = namespaceManager.GetQueue(QueueConnector.QueueName);
            ViewBag.MessageCount = queue.MessageCount;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Submit()
        {
            var namespaceManager = QueueConnector.CreateNamespaceManager();
            var queue = namespaceManager.GetQueue(QueueConnector.QueueName);
            ViewBag.MessageCount = queue.MessageCount;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(OnlineOrder order)
        {
            if (ModelState.IsValid)
            {
                var message = new BrokeredMessage(order);
                QueueConnector.OrdersQueueClient.Send(message);
                return RedirectToAction("Submit");
            }
            else
            {
                return View(order);
            }
        }
        public ActionResult Receive()
        {
            var namespaceManager = QueueConnector.CreateNamespaceManager();
            var queue = namespaceManager.GetQueue(QueueConnector.QueueName);

            OnlineOrder order = null;
            List<OnlineOrder> orders = new List<OnlineOrder>();
            if (queue.MessageCount != 0)
            {
                QueueConnector.OrdersQueueClient.OnMessage((receivedMessage) =>
                {
                    order = receivedMessage.GetBody<OnlineOrder>();
                    receivedMessage.Complete();
                });

                return View(order);
            }
            else
            {
                return RedirectToAction("Index");
            }
                
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}