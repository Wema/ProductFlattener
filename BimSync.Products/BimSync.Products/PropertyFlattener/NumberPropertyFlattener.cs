using BimSync.Products;
using BimSync.Products.Models;
using BimSync.Products.PropertyFlattener;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BimSync.Products.PropertyFlattener
{
   public class NumberPropertyFlattener : IPropertyFlattener
   {

      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<ProductProperty<decimal>>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            var valueProperty = jObj.Property("value");
            var typeProperty = jObj.Property("type");
            var unitProperty = jObj.Property("unit");
            if(valueProperty != null && typeProperty != null)
            {
               var type = typeProperty.Value.ToObject<string>();
               var value = valueProperty.Value.ToObject<decimal>();
               var unit = unitProperty == null ? null : unitProperty.Value.ToObject<string>();
               properties.Add(new ProductProperty<Decimal>(type, desc, value, unit, group: group, parents: parents));
            }
         }
         return properties;
      }
   }
}
