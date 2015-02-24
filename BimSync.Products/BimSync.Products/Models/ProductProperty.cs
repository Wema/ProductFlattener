using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BimSync.Products.Models
{
   public class ProductProperty<T> : BimSync.Products.Models.IProductProperty
   {
      public ProductProperty(string type, string desc, T value, string unit = null, string group = null, List<string> parents = null)
      {
         Type = type;
         Description = desc;
         Value = value;
         Unit = unit;
         PropertySet = group;
         Parents = parents;
      }
      public string Type { get; private set; }
      public string Description { get; private set; }
      public object Value { get; private set; }
      public string Unit { get; private set; }

      public string PropertySet { get; private set; }

      public List<string> Parents { get; private set; }
   }
}
