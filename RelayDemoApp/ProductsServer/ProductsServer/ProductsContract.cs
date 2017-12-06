using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ProductsServer
{
    [DataContract]
    public class ProductData
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Quantity { get; set; }
    }
    [ServiceContract]
    interface IProducts
    {
        [OperationContract]
        IList<ProductData> GetProducts();
        [OperationContract]
        void UpdataProduct(ProductData product);
    }
    interface IProductsChannel : IProducts, IClientChannel
    {

    }
}
