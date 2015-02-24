using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class ArrayPropertyFlattener : IPropertyFlattener
   {
      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<IProductProperty>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            var value = jObj.Property("value");
            if(value != null && value.Value.Type == JTokenType.Array)
            {
               JArray jArray = (JArray)value.Value;
               if(desc != null)
               {
                  if(parents == null)
                     parents = new List<string>(new string[] { desc });
                  else
                     parents.Add(desc);
               }
               var newList = parents != null ? new List<string>(parents) : null;
               foreach(var item in jArray.Where(j => j.Type == JTokenType.Object))
               {
                  JObject childjObj = (JObject)item;
                  if(childjObj.Property("type") != null)
                  {
                     IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(childjObj.Property("type").Value.ToObject<string>());
                     properties.AddRange(flattener.Flatten(childjObj, null, group, newList));
                  }
               }
            }
         }
         return properties;
      }
   }
}
