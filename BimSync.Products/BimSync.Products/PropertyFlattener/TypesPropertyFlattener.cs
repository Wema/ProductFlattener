using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class TypesPropertyFlattener : IPropertyFlattener
   {
      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<IProductProperty>();
         if(jToken.Type == JTokenType.Array)
         {
            JArray jArray = (JArray)jToken;
            foreach(var item in jArray.Where(j => j.Type == JTokenType.Object))
            {
               JObject jObj = (JObject)item;
               if(jObj.Property("attributes") != null)
               {
                  IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For("attributes");
                  properties.AddRange(flattener.Flatten(jObj.Property("attributes").Value, null, group: group));
               }
               if(jObj.Property("name") != null)
               {
                  properties.Add(new ProductProperty<String>("string", "Name", jObj.Property("name").Value.ToObject<string>(), null, group, parents));
               }

            }
         }
         return properties;
      }
   }
}
