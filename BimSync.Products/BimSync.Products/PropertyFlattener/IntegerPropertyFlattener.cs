using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class IntegerPropertyFlattener : IPropertyFlattener
   {

      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<ProductProperty<int>>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            var valueProperty = jObj.Property("value");
            var typeProperty = jObj.Property("type");
            var unitProperty = jObj.Property("unit");
            if(valueProperty != null && typeProperty != null)
            {
               var type = typeProperty.Value.ToObject<string>();
               var value = valueProperty.Value.ToObject<int>();
               var unit = unitProperty == null ? null : unitProperty.Value.ToObject<string>();
               properties.Add(new ProductProperty<int>(type, desc, value, unit, group: group, parents: parents));
            }
         }
         return properties;
      }
   }
}
