using System.Collections.Generic;
using System.IO;

namespace Site.Utilities
{
    public static class DBConfigHelper
    {
        public static Dictionary<string, string> dbConfigDictionary;
        public static void InitializeDBConfig()
        {
            var dbConfigArray = File.ReadAllLines("dbconfig.ini");
            dbConfigDictionary = new Dictionary<string, string>()
            {
                ["adminName"] = dbConfigArray[0], 
                ["adminEmail"] = dbConfigArray[1] ,
                ["adminPassword"] = dbConfigArray[2],
                ["applicationPassword"] = dbConfigArray[3]
            };
        }
    }
}