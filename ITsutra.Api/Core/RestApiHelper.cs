using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;

//using Accounts.Test;

namespace ITsutra.Api.Core
{
    public static class RestApiHelper
    {
        public static RestClient client;
        public static RestRequest restRequest;
        public static string baseUrl;
        public static string authorizationToken;
        

        public static void SetBaseUrl(string url)
        {
            baseUrl = url;
        }

        public static RestClient Seturl(string endpoint)
        {
            var url = baseUrl + endpoint;
            return client = new RestClient(url);
        }

        public static void CreateRequesttype(string typeMethod, string Authorized, string fileName = null, Table table = null)
        {
            if (typeMethod == "POST")
            {
                CreatePostRequest(fileName, Authorized, table);
            }
            else if (typeMethod == "PUT")
            {
                CreatePutRequest(fileName, Authorized, table);
            }
        }

        public static RestRequest CreateGetRequest(string Authorized)
        {
            restRequest = new RestRequest(Method.GET);
            if (Authorized == "Authorized")
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\AccessToken.json";
                var fileData = File.ReadAllText(path);
                var data = (JObject)JsonConvert.DeserializeObject(fileData);
                authorizationToken = data["accessToken"].Value<string>();
                authorizationToken = CryptoEngine.Decrypt(authorizationToken, "sblw-3hn8-sqoy19");
                restRequest.AddHeader("Authorization", "Bearer " + authorizationToken);
            }

            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public static RestRequest CreateDeleteRequest(string Authorized)
        {
            restRequest = new RestRequest(Method.DELETE);
            if (Authorized == "Authorized")
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\AccessToken.json";
                var fileData = File.ReadAllText(path);
                var data = (JObject)JsonConvert.DeserializeObject(fileData);
                authorizationToken = data["accessToken"].Value<string>();
                authorizationToken = CryptoEngine.Decrypt(authorizationToken, "sblw-3hn8-sqoy19");
                restRequest.AddHeader("Authorization", "Bearer " + authorizationToken);
            }

            restRequest.AddHeader("Accept", "application/json");

            return restRequest;
        }

        public static RestRequest CreatePutRequest(string jsonFileName, string Authorized, Table table)
        {
            restRequest = new RestRequest(Method.PUT);
            if (Authorized == "Authorized")
            {
                string accessPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\AccessToken.json";
                var accessData = File.ReadAllText(accessPath);
                var accessObject = (JObject)JsonConvert.DeserializeObject(accessData);
                authorizationToken = accessObject["accessToken"].Value<string>();
                authorizationToken = CryptoEngine.Decrypt(authorizationToken, "sblw-3hn8-sqoy19");
                restRequest.AddHeader("Authorization", "Bearer " + authorizationToken);
            }

            restRequest.AddHeader("Accpet", "application/json");
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\" + jsonFileName + ".json";
            var fileData = File.ReadAllText(path);
            var data = (JObject)JsonConvert.DeserializeObject(fileData);
            if (table != null)
            {
                foreach (var tab in table.Rows)
                {
                    data[tab[0]] = tab[1];
                }
            }
            var dat = data.ToString();
            restRequest.AddJsonBody(dat);
            return restRequest;
        }

        public static RestRequest CreatePostRequest(string jsonFileName, string Authorized, Table table = null)
        {
            restRequest = new RestRequest(Method.POST);
            if (Authorized == "Authorized")
            {
                string accessPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\AccessToken.json";
                var accessData = File.ReadAllText(accessPath);
                var accessObject = (JObject)JsonConvert.DeserializeObject(accessData);
                authorizationToken = accessObject["accessToken"].Value<string>();
                authorizationToken = CryptoEngine.Decrypt(authorizationToken, "sblw-3hn8-sqoy19");
                restRequest.AddHeader("Authorization", "Bearer " + authorizationToken);
            }

            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\" + jsonFileName + ".json";
            var fileData = File.ReadAllText(path);
            var data = (JObject)JsonConvert.DeserializeObject(fileData);
            if (table != null)
            {
                foreach (var tab in table.Rows)
                {
                    data[tab[0]] = tab[1];
                }
            }
            var dat = data.ToString();
            restRequest.AddJsonBody(dat);
            return restRequest;
        }

        public static IRestResponse GetResponse()
        {
            return client.Execute(restRequest);
        }
    }
}