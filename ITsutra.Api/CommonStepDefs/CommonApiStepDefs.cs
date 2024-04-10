using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Configuration;
using System.IO;
using ITsutra.Api.Core;
using System.ComponentModel;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;

namespace ITsutra.Api.CommonStepDefs
{
    [Binding]
    public sealed class CommonApiStepDefs
    {
        public readonly TestLog test = new TestLog();

        [Given(@"I have an endpoint (.*)")]
        public void GivenAUserIsInSmsPageApiGetFromNumber(string endpoint)
        {
            test.Log("Pass", "We are in Given");
            RestApiHelper.Seturl(endpoint);
        }

        [Given(@"I am in (.*)")]
        public void GivenIAmInReportingServerHttpsLocalhost(string server)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\Configuration.json";
            var fileData = File.ReadAllText(path);
            var data = (JObject)JsonConvert.DeserializeObject(fileData);
            var str = data[server].Value<string>();
            RestApiHelper.SetBaseUrl(str);
        }

        [Given(@"I have parameter in an endpoint (.*)")]
        public void GivenIHaveParameterInAEndpoint(string endpoint, Table table)
        {
            test.Log("Pass", "We are in endpoint given");
            endpoint = endpoint + "?";
            if (table.Rows.Count > 0)
            {
                var count = table.Rows.Count();
                var cnt = 0;
                var dat = "{";
                foreach (var tab in table.Rows)
                {
                    cnt++;
                    endpoint = endpoint + tab[0] + "=" + tab[1];
                    dat = dat + "\"" + tab[0] + "\":\"" + tab[1] + "\"";
                    if (cnt < count)
                    {
                        endpoint += "&&";
                        dat += ",";
                    }
                }
                dat += "}";
                string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\Params.json";
                File.WriteAllText(path, dat);
            }
            RestApiHelper.Seturl(endpoint);
        }

        [When(@"I call (.*) method with (.*) Api (.*)")]
        public void WhenICallMethodOfApi(string typeMethod, string Authorized, string fileName = null, Table table = null)
        {
            test.Log("Pass", "We are in get method");
            RestApiHelper.CreateRequesttype(typeMethod, Authorized, fileName, table);
        }

        [When(@"I call (.*) methods of api (.*)")]
        public void WhenICallGETMethodsOfApi(string method, string Authorized)
        {
            test.Log("Pass", "We are in post/put method");
            if (method == "GET")
                RestApiHelper.CreateGetRequest(Authorized);
            else if (method == "DELETE")
                RestApiHelper.CreateDeleteRequest(Authorized);
        }

        public static void AuthorizeApi(string Authorized)
        {
            string server = "Accounts";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\Configuration.json";
            var fileData = File.ReadAllText(path);
            var data = (JObject)JsonConvert.DeserializeObject(fileData);
            var str = data[server].Value<string>();
            RestApiHelper.SetBaseUrl(str);
            RestApiHelper.Seturl("/api/TokenAuth/Authenticate");
            RestApiHelper.CreatePostRequest("Authenticate", Authorized);
            var apiResponse = RestApiHelper.GetResponse();
            data = (JObject)JsonConvert.DeserializeObject(apiResponse.Content);
            var filteredJson = data["result"];
            RestApiHelper.authorizationToken = filteredJson["accessToken"].Value<string>();
            var cipher = CryptoEngine.Encrypt(RestApiHelper.authorizationToken, "sblw-3hn8-sqoy19");
            RestApiHelper.authorizationToken = cipher;
            var stringout = "{\"accessToken\":\"" + RestApiHelper.authorizationToken + "\"}\n";
            path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\AccessToken.json";
            File.WriteAllText(path, stringout);
        }

        [Then(@"I check StatusCode as (.*)")]
        public void ThenICheckStatusCodeAs(int code)
        {
            test.Log("Info", "Worked");
            Console.WriteLine("Worked");
            var apiResponse = RestApiHelper.GetResponse();
            Assert.That((Int32)apiResponse.StatusCode, Is.EqualTo(code), "Status Code " + code + " Doesn't Matched");
        }
    }
}