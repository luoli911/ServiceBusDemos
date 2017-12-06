using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductsPortal.Models;
using System.ServiceModel;
using Microsoft.ServiceBus;
using ProductsServer;
using ProductsPortal.Extensions;

namespace ProductsPortal.Controllers
{
    public class HomeController : Controller
    {
        static ChannelFactory<IProductsChannel> channelFactory;
        static HomeController()
        {
            channelFactory = new ChannelFactory<IProductsChannel>(new NetTcpRelayBinding(),
                "sb://alicerelay1.servicebus.windows.net/products");
            channelFactory.Endpoint.EndpointBehaviors.Add(new TransportClientEndpointBehavior
            {
                TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                    "RootManageSharedAccessKey", "pfd6wGd88RtQRhkGWLzNAxumQYvhy6udMd9GooD5kks=")
            });
        }
        public ActionResult Index(string Identifier, string ProductName)
        {
            
            using(IProductsChannel channel = channelFactory.CreateChannel())
            {
                return View(from prod in channel.GetProducts()
                            select new Product { Id = prod.Id, Name = prod.Name, Quantity = prod.Quantity });
            }
          
        }
        public ActionResult Edit(string id)
        {
            using (IProductsChannel channel = channelFactory.CreateChannel())
            {
                var product = from prod in channel.GetProducts()
                              where prod.Id == id
                              select new Product { Id = prod.Id, Name = prod.Name, Quantity = prod.Quantity };
                return View(product.First());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                using (IProductsChannel channel = channelFactory.CreateChannel())
                {
                    var productData = product.MapTo(product);
                    channel.UpdataProduct(productData);
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}