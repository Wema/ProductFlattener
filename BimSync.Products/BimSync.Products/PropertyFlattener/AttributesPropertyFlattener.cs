using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BimSync.Products.PropertyFlattener
{
   public class AttributesPropertyFlattener : IPropertyFlattener
   {
      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
        
         var properties = new List<IProductProperty>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            foreach(var property in jObj.Properties().Where(p => p.Value.Type == JTokenType.Object))
            {
               var valueObj = (JObject)property.Value;
               var valueProperty = valueObj.Property("type");
               if(valueProperty != null)
               {
                  IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(valueProperty.Value.ToObject<string>());
                  properties.AddRange(flattener.Flatten(valueObj, property.Name,group, parents));
               }

            }
         }
         return properties;
      }
   }
}
