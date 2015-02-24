using BimSync.Products.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products.PropertyFlattener
{
   public class PropertySetsPropertyFlattener : IPropertyFlattener
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
                  properties.AddRange(flattener.Flatten(jObj.Property("attributes").Value, null, null,null));
               }
               if(jObj.Property("properties") != null)
               {
                  JObject propertiesObj = (JObject)jObj.Property("properties").Value;
                  foreach(var prop in propertiesObj.Properties().Where(p => p.Value.Type == JTokenType.Object))
                  {

                     var childValue = (JObject)prop.Value;
                     if(childValue.Property("ifcType") != null)
                     {
                        IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(childValue.Property("ifcType").Value.ToObject<string>());
                        properties.AddRange(flattener.Flatten(childValue, prop.Name, jObj.Property("name").ToObject<String>(), null ));
                     }
                  }
               }

            }
         }
         return properties;
      }
   }
}
