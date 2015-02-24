using BimSync.Products;
using BimSync.Products.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BimSync.Products.PropertyFlattener
{
   public interface IPropertyFlattener
   {
      IEnumerable<IProductProperty> Flatten(JToken jToken, string desc, string group = null, List<string> parents = null );
   }
}
