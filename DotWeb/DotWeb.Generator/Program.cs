using System;
using System.Configuration;
using System.Reflection;

namespace DotWeb.Generator
{
    /// <summary>
    /// <para>Meta data generator for DotWeb.</para>
    /// <para>The following connection string names are required in configuration file: AppDb and DotWebDb.
    /// Also, appSettings appId is required. Meanwhile, appSettings appName is optional, it can be used to
    /// generate application's name, if it is not already exist in DotWebDb.</para>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Meta Data Generator for DotWeb. Version " + Assembly.GetExecutingAssembly().GetName().Version);
            if (ConfigurationManager.ConnectionStrings["AppDb"] == null || ConfigurationManager.ConnectionStrings["DotWebDb"] == null)
                Console.WriteLine("ERROR: missing connection string name AppDb or DotWebDb in configuration file. Please fix!");
            if (ConfigurationManager.AppSettings["appId"] == null)
                Console.WriteLine("ERROR: missing appSettings appId key in configuration file. Please fix!");
            var dbInspector = new DbInspector();
            dbInspector.GenerateFromDb("AppDb");
            Console.WriteLine("DONE. Press any key to exit this application.");
            Console.ReadKey();
        }
    }
}
