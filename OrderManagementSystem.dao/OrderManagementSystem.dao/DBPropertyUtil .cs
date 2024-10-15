using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString(string propertyFileName)
        {
            string[] lines = File.ReadAllLines(propertyFileName);
            foreach (var line in lines)
            {
                if (line.StartsWith("ConnectionString"))
                {
                    return line.Split('=')[1].Trim();
                }
            }
            throw new FileNotFoundException("Connection string not found in the property file.");
        }
    }
}
