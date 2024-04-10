using ITsutra.Api.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;

namespace Accounts.Test.NewFolder
{
    [Binding]
    public class StepDefs
    {
        [Then(@"I verify api response in (.*)")]
        public void ThenIGetApiResponseInJsonFormat(string output = null)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\NameSpace.json";
            var fileData = File.ReadAllText(path);
            var data = (JObject)JsonConvert.DeserializeObject(fileData);
            var str = data["TestQuery"].Value<string>();
            var apiResponse = RestApiHelper.GetResponse();
            if (output == "booleanTest")
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "200 status code is not throwing");
                Assert.That(apiResponse.Content, Is.EqualTo("true"), "Output is false");
            }
            else
            {
                var tag = FeatureContext.Current.FeatureInfo.Tags;
                string tags = tag[0];
                var pageNamespace = str + ".Dto." + tags + "." + output;
                var queryNamespace = str + ".Query." + tags + "Query";
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                Type queryObject = executingAssembly.GetType(queryNamespace);
                Type pageObjectTypes = executingAssembly.GetType(pageNamespace);
                var expected = queryObject.GetMethod(output);
                var method = expected.Invoke(queryNamespace, null);
                object expectedJson = JsonConvert.DeserializeObject(method.ToString(), pageObjectTypes);
                object ActualResponse = JsonConvert.DeserializeObject(apiResponse.Content, pageObjectTypes);
                Assert.That(ActualResponse.ToString(), Is.EqualTo(expectedJson.ToString()), "Expected value and Api Response MisMatched");
                Assert.That(apiResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "200 status code is not throwing");
            }
        }
    }
}