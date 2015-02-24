using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class PropertySingleValueFlattener : IPropertyFlattener
   {
      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<IProductProperty>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            foreach(var childProp in jObj.Properties().Where(p => p.Value.Type == JTokenType.Object))
            {
               IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(childProp.Name);
               properties.AddRange(flattener.Flatten(childProp.Value, desc, group, parents));
            }
         }
         return properties;
        
      }
   }
}
