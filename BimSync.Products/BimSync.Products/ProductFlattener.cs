using BimSync.Products.Models;
using BimSync.Products.PropertyFlattener;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products
{
   public class ProductFlattener
   {
      public ProductFlattener()
      {

      }
      public Product Flatten(JObject jObj)
      {
         var product = jObj.ToObject<Product>();
         List<IProductProperty> props = new List<IProductProperty>();
         foreach(var group in jObj.Properties().Where(p => p.Value.Type == JTokenType.Object || p.Value.Type == JTokenType.Array))
         {
            IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(group.Name);
            var properties = flattener.Flatten(group.Value, string.Empty, null);
            product.AddProperties(group.Name, properties);
         }
        
         return product;
      }


   }
}
