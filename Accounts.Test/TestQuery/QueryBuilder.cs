using System;
using Accounts.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;

//using Supporting.Data;
using ITsutra.Api.Core;
using System.Data.SqlClient;

namespace Accounts.Test.TestQuery
{
    public class QueryBuilder
    {
        public static string ParameterData()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\Params.json";
            string fileData = File.ReadAllText(path);
            string data = "";
            File.WriteAllText(path, data);
            return fileData;
        }

        public static AccountDbContext AccountDatabase()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\JsonInput\Configuration.json";
            var fileData = File.ReadAllText(path);
            var data = (JObject)JsonConvert.DeserializeObject(fileData);
            var str = data["AccountsConnectionStrings"].Value<string>();
            RestApiHelper.SetBaseUrl(str);
            var optionsBuilder = new DbContextOptionsBuilder<AccountDbContext>();
            optionsBuilder.UseSqlServer(str);
            AccountDbContext _DbContext = new AccountDbContext(optionsBuilder.Options);
            return _DbContext;
        }

        public static string JsonSqlExecutor(string sql)
        {
            sql = sql + " FOR JSON PATH";
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=sql-itsutradev.database.windows.net;Initial Catalog=sqldb-Account-dev;User ID=itsutraDev;Password=c0mfOrtable!";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            string res = dataReader.GetString(0);
            cnn.Close();
            return res;
        }
    }
}