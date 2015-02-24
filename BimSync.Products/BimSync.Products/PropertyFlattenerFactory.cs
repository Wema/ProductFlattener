using BimSync.Products.PropertyFlattener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimSync.Products
{
   public class PropertyFlattenerFactory
   {
      private Dictionary<string, IPropertyFlattener> flatteners = new Dictionary<string,IPropertyFlattener>();
      private static PropertyFlattenerFactory factory;

      private PropertyFlattenerFactory()
      {
         flatteners.Add("unknown", new UnKnowPropertyFlattener());
      }

      public static PropertyFlattenerFactory Flattener()
      {
         if(factory == null)
         {
            factory = new PropertyFlattenerFactory();
         }
         return factory;
      }

      public void Add(string type, IPropertyFlattener flattener)
      {
         flatteners.Add(type, flattener);
      }

     
      public IPropertyFlattener For(string type)
      {
         if(flatteners.ContainsKey(type))
         {
            return flatteners[type];
         }
         return flatteners["unknown"];

      }


      public void Clear()
      {
         flatteners.Clear();
         flatteners.Add("unknown", new UnKnowPropertyFlattener());
      }
   }
}
