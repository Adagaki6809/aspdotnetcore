using System.Collections.Generic;
using System.IO;

namespace Site.Utilities
{
    public static class DBConfigHelper
    {
        public static Dictionary<string, string> GetDBConfig()
        {
            var streamReader = new StreamReader(File.OpenRead("dbconfig.ini"));
            var dbConfigArray = streamReader.ReadToEnd().Split("\n");
            var dbConfigDictionary = new Dictionary<string, string>()
            {
                ["adminName"] = dbConfigArray[0], 
                ["adminEmail"] = dbConfigArray[1] ,
                ["adminPassword"] = dbConfigArray[2],
                ["applicationPassword"] = dbConfigArray[3]
            };
            return dbConfigDictionary;
        }
    }
}