using ProductsPortal.Models;
using ProductsServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsPortal.Extensions
{
    public static class ProductExtention
    {
        public static ProductData MapTo(this Product entity,Product product)
        {
            ProductData productData = new ProductData();
            productData.Id = product.Id;
            productData.Name = product.Name;
            productData.Quantity = product.Quantity;
            return productData;
        }
    }
}