using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BimSync.Products.Models
{
   public interface IProductProperty
   {
      [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
      string PropertySet { get; }
      [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
      List<string> Parents { get; }
      string Description { get; }
      string Type { get; }
      [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
      string Unit { get; }
      object Value { get;}
   }
}
