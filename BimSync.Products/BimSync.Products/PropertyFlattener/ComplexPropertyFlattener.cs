using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class ComplexPropertyFlattener : IPropertyFlattener
   {
      public IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null)
      {
         var properties = new List<IProductProperty>();
         if(jToken.Type == JTokenType.Object)
         {
            JObject jObj = (JObject)jToken;
            if(jObj.Property("properties") != null && jObj.Property("properties").Value.Type == JTokenType.Object)
            {
               var propertiesObj = (JObject)jObj.Property("properties").Value;
               if(desc != null)
               {
                  if(parents == null)
                     parents = new List<string>(new string[] { desc });
                  else
                     parents.Add(desc);
               }
               var newList = parents != null ? new List<string>(parents) : null;
               foreach(var prop in propertiesObj.Properties().Where(p => p.Value.Type == JTokenType.Object))
               {

                  var childValue = (JObject)prop.Value;
                  if(childValue.Property("ifcType") != null)
                  {
                     IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(childValue.Property("ifcType").Value.ToObject<string>());
                    
                     properties.AddRange(flattener.Flatten(childValue, prop.Name, group, newList));
                  }
               }
            }
           
         }
         return properties;
      }
   }
}
