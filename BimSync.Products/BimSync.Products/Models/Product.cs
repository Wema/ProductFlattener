using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BimSync.Products;
using Newtonsoft.Json;

namespace BimSync.Products.Models
{
   public class Product
   {

      private readonly Dictionary<string, List<IProductProperty>> properties;
      public Product(Dictionary<string,List<IProductProperty>> properties = null)
      {
         if(properties == null)
         {
            properties = new Dictionary<string,List<IProductProperty>>();
         }
         this.properties = properties;
      }
      [JsonProperty("objectId")]
      public int Id { get; set; }
      [JsonProperty("name")]
      public string Name { get; set; }
      [JsonProperty("globalId")]
      public string GlobalId { get; set; }
      [JsonProperty("ifcType")]
      public string Type { get; set; }
      [JsonProperty("properties")]
      public IDictionary<string, List<IProductProperty>> Properties { get { return this.properties; } }
      public void AddProperties(string key,  IEnumerable<IProductProperty> properties)
      {
         this.properties.Add(key, properties.ToList());
      }
   }
}
