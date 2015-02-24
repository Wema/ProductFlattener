using BimSync.Products;
using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BimSync.Products.PropertyFlattener
{
   /// <summary>
   /// Parser for string and enum
   /// </summary>
   public class BasicPropertyFlattener : IPropertyFlattener
   {

      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {

         var properties = new List<ProductProperty<String>>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            var valueProperty = jObj.Property("value");
            var type = jObj.Property("type");
            if(valueProperty != null && type != null)
            {
               var typeValue = type.Value.ToObject<string>();
               var valueValue = valueProperty.Value.ToObject<string>();
               properties.Add(new ProductProperty<String>(typeValue, desc, valueValue, group: group, parents: parents));
            }
         }
         return properties;
      }
   }
}
