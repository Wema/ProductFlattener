using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimsync.Products.Tests.Helpers
{
   public class TestFiles
   {
      public static string GetFile(string name)
      {
         var data = string.Empty;
         var file = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\test_data\" + name;
         if(System.IO.File.Exists(file))
         {
            data = System.IO.File.ReadAllText(file);
         }
         return data;
      }

   }
}
