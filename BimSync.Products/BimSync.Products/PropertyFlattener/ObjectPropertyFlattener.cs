using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class ObjectPropertyFlattener : IPropertyFlattener
   {
      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null
         )
      {
         var properties = new List<IProductProperty>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            var value = jObj.Property("value");
            if(value != null && value.Value.Type == JTokenType.Object && ((JObject)value.Value).Property("attributes") != null)
            {
               var att = ((JObject)value.Value).Property("attributes");
               IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For("attributes");
               if(desc != null)
               {
                  if(parents == null)
                     parents = new List<string>(new string[] { desc });
                  else
                     parents.Add(desc);
               }
               var newList = parents != null ? new List<string>(parents) : null;
               properties.AddRange(flattener.Flatten(att.Value, null, group, newList));
            }
         }
         return properties;
      }
   }

   public class Constants
   {
      public static string Seperator = "__";
   }
}
