using System;
using System.Collections.Generic;
using System.IO;

namespace Site.Utilities
{
    public static class DBConfigHelper
    {
        public static Dictionary<string, string> dbConfigDictionary;
        public static void InitializeDBConfig()
        {
            //var dbConfigArray = File.ReadAllLines("dbconfig.ini");
            dbConfigDictionary = new Dictionary<string, string>()
            {
                ["adminName"] = Environment.GetEnvironmentVariable("ADMIN_NAME"), //dbConfigArray[0],
                ["adminEmail"] = Environment.GetEnvironmentVariable("ADMIN_EMAIL"), //dbConfigArray[1],
                ["adminPassword"] = Environment.GetEnvironmentVariable("ADMIN_PASSWORD"), //dbConfigArray[2],
                ["applicationPassword"] = Environment.GetEnvironmentVariable("APP_PASSWORD") //dbConfigArray[3]
            };
        }
    }
}