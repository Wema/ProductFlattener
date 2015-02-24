using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class NominalValueFlattener : IPropertyFlattener
   {
      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<IProductProperty>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            if(jObj.Property("type") != null)
            {
               IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(jObj.Property("type").Value.ToObject<string>());
               properties.AddRange(flattener.Flatten(jObj, desc, group, parents));
            }

         }
         return properties;

      }
   }
}
