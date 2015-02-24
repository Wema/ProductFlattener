using BimSync.Products.Models;
using BimSync.Products.PropertyFlattener;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimsync.Products.Tests
{
   [TestFixture]
   public class PropertyParserTest
   {
      /*{
      "ObjectType": {
        "type": "string",
        "ifcType": "IfcLabel",
        "value": "300X200"
      } 
       */
      [Test]
      public void FlattenStingPropertyTest()
      {
         var expected = new ProductProperty<String>("string", "ObjectType", "300X200");
         var parser = new BasicPropertyFlattener();
         var testData = @"{""ObjectType"": { ""type"": ""string"",""ifcType"": ""IfcLabel"",""value"": ""300X200""}}";
         var jObj = (JObject)JObject.Parse(testData).Property("ObjectType").Value;
         var property = parser.Flatten(jObj, "ObjectType").First();
         Assert.AreEqual(property.Description , expected.Description);
         Assert.AreEqual(property.Value , expected.Value);
         Assert.AreEqual(property.Type, expected.Type);
         Assert.IsNull(property.Unit);
      }
      /*
       * {
          "OuterSurfaceArea": {
            "ifcType": "IfcQuantityArea",
            "value": {
              "type": "number",
              "ifcType": "IfcAreaMeasure",
              "value": 9.2,
              "unit": "m²"
            }
          }
       */
      [Test]
      public void FlattenNumberPropertyTest()
      {
         var testdata = @"{""OuterSurfaceArea"": {""ifcType"": ""IfcQuantityArea"",""value"": {""type"": ""number"",""ifcType"": ""IfcAreaMeasure"",""value"": 9.2,""unit"": ""m²""}}}";
         var expected = new ProductProperty<Decimal>("number", "OuterSurfaceArea", 9.2m, "m²");
         var parser = new NumberPropertyFlattener();
         var jObj = (JObject)((JObject)JObject.Parse(testdata).Property("OuterSurfaceArea").Value).Property("value").Value;
         var property = parser.Flatten(jObj, "OuterSurfaceArea").First();
         Assert.AreEqual(property.Description, expected.Description);
         Assert.AreEqual(property.Value, expected.Value);
         Assert.AreEqual(property.Type, expected.Type);
         Assert.AreEqual(property.Unit, expected.Unit);
      }

      [Test]
      public void FlattenBooleanPropertyTest()
      {
         var expected = new ProductProperty<bool>("boolean", "LoadBearing", true);
         var parser = new BooleanPropertyFlattener();
         var testdata = @"{""LoadBearing"": {""ifcType"": ""IfcPropertySingleValue"",""nominalValue"": {""type"": ""boolean"",""ifcType"": ""IfcBoolean"",""value"": true}}}";
         var jObj = (JObject)((JObject)JObject.Parse(testdata).Property("LoadBearing").Value).Property("nominalValue").Value;
         var property = parser.Flatten(jObj, "LoadBearing").First();
         Assert.AreEqual(property.Description, expected.Description);
         Assert.AreEqual(property.Value, expected.Value);
         Assert.AreEqual(property.Type, expected.Type);
         Assert.IsNull(property.Unit);
      }
      [Test]
      public void FlattenEnumPropertyTest()
      {
         var expected = new ProductProperty<String>("enum", "PredefinedType", "NOTDEFINED");
         var testdata = @"{""PredefinedType"": {""type"": ""enum"",""ifcType"": ""IfcBeamTypeEnum"",""value"": ""NOTDEFINED""}}";
         var parser = new BasicPropertyFlattener();
         var jObj = (JObject)JObject.Parse(testdata).Property("PredefinedType").Value;
         var property = parser.Flatten(jObj, "PredefinedType").First();
         Assert.AreEqual(property.Description, expected.Description);
         Assert.AreEqual(property.Value, expected.Value);
         Assert.AreEqual(property.Type, expected.Type);
         Assert.IsNull(property.Unit);
      }
      [Test]
      public void FlattenObjectPropertyTest()
      {

      }
      [Test]
      public void FlattenArrayPropertyTest()
      {

      }







   }
}
