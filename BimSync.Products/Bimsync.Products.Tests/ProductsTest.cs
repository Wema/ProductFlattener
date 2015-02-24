using BimSync.Products;
using BimSync.Products.Models;
using BimSync.Products.PropertyFlattener;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimsync.Products.Tests
{
   [TestFixture]
   public class ProductsTest
   {
      [Test]
      public void FlattenBeamProductTest()
      {

         BuildFactory();

         var expected = new Product()
         {
            GlobalId = "1ldHk5baXAWBB4kMiZXzAO",
            Id = 912184375,
            Name = "Balk-004",
            Type = "IfcBeam"
         };
         var testdata = Helpers.TestFiles.GetFile("beam.json");
         var jObj = JObject.Parse(testdata);
         var flattener = new ProductFlattener();
         var product = flattener.Flatten(jObj);
         Assert.AreEqual(expected.Id, product.Id);
         Assert.AreEqual(expected.GlobalId, product.GlobalId);
         Assert.AreEqual(expected.Type, product.Type);
         Assert.AreEqual(expected.Name, product.Name);
         string json = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
         File.WriteAllText(@"C:\Temp\beam.json", json);
         PropertyFlattenerFactory.Flattener().Clear();


      }
      [Test]
      public void FlattenWallProductTest()
      {
         BuildFactory();
         //materials


         var expected = new Product()
         {
            GlobalId = "1xR1Dku9TCdxjX7pW7MV0W",
            Id = 1198587222,
            Type = "IfcWallStandardCase"
         };
         var testdata = Helpers.TestFiles.GetFile("wall.json");
         var jObj = JObject.Parse(testdata);
         var flattener = new ProductFlattener();
         var product = flattener.Flatten(jObj);
         Assert.AreEqual(expected.Id, product.Id);
         Assert.AreEqual(expected.GlobalId, product.GlobalId);
         Assert.AreEqual(expected.Type, product.Type);
         Assert.IsNull(product.Name);
         string json = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
         File.WriteAllText(@"C:\Temp\wall.json", json);
         PropertyFlattenerFactory.Flattener().Clear();
      }


      private void BuildFactory()
      {
         PropertyFlattenerFactory.Flattener().Add("string", new BasicPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("enum", new BasicPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("number", new NumberPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("boolean", new NumberPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("attributes", new AttributesPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("quantitySets", new QuantitiesPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("propertySets", new PropertySetsPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("nominalValue", new NominalValueFlattener());
         PropertyFlattenerFactory.Flattener().Add("types", new TypesPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("materials", new MaterialsPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("presentationLayers", new MaterialsPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("IfcPropertySingleValue", new PropertySingleValueFlattener());
         PropertyFlattenerFactory.Flattener().Add("IfcComplexProperty", new ComplexPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("integer", new IntegerPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("array", new ArrayPropertyFlattener());
         PropertyFlattenerFactory.Flattener().Add("object", new ObjectPropertyFlattener());
      }

   }
}
