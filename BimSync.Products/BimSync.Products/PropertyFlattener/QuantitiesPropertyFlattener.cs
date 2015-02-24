using BimSync.Products.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace BimSync.Products.PropertyFlattener
{
   public class QuantitiesPropertyFlattener : IPropertyFlattener
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
                  properties.AddRange(flattener.Flatten(jObj.Property("attributes").Value, null, group : group));
               }
               if(jObj.Property("quantities") != null)
               {
                  JObject quantatiesObj = (JObject)jObj.Property("quantities").Value;
                  foreach(var prop in quantatiesObj.Properties())
                  {
                     var converter = new ExpandoObjectConverter();

                     dynamic propValue = JsonConvert.DeserializeObject<ExpandoObject>(prop.Value.ToString(), converter);
                     IPropertyFlattener flattener = PropertyFlattenerFactory.Flattener().For(propValue.value.type);
                     parents = new List<string>(new string[] { jObj.Property("name").Value.ToObject<string>() });
                     properties.AddRange(flattener.Flatten(((JObject)prop.Value).Property("value").Value, prop.Name, group, parents));
                  }
               }
               
            }
         }
         return properties;
      }
   }
}
