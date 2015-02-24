using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BimSync.Products;
using BimSync.Products.Models;
using Newtonsoft.Json.Linq;

namespace BimSync.Products.PropertyFlattener
{
   public class BooleanPropertyFlattener : IPropertyFlattener
   {

      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<ProductProperty<bool>>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            var valueProperty = jObj.Property("value");
            var typeProperty = jObj.Property("type");
            if(valueProperty != null && typeProperty != null)
            {
               var type = typeProperty.Value.ToObject<string>();
               var value = valueProperty.Value.ToObject<bool>();
               properties.Add(new ProductProperty<bool>(type, desc, value, null, group, parents));
            }
         }
         return properties;
      }
   }
}
