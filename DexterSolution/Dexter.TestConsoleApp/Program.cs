using Dexter.ConnectionExtensions;
using Dexter.Factory;
using DexterCfg.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Dexter.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string sConnType = ConfigurationManager.AppSettings["connType"];
            sConnType = sConnType ?? "mysql";
            string sConnStr = ConfigurationManager.AppSettings["connString"];
            sConnStr = sConnStr ?? "Server=127.0.0.1;database=mst_stock;Uid=root;Pwd=my123123;";

            foreach (var item in DxCfgConnectionFactory.Instance.SettingKeys)
            {
                Console.WriteLine(string.Format("{0} : {1}", item, DxCfgConnectionFactory.Instance[item]));
            }
            //Console.WriteLine(DxCfgConnectionFactory.Instance[""]);

            IDbConnection conn = DxCfgConnectionFactory.Instance.GetConnection(sConnType);
            conn.ConnectionString = sConnStr;

            conn.Open();
            Console.WriteLine("Connection opened.");
            object obj = conn.ExecuteScalar("SELECT COUNT(1) FROM stock;");
            Console.WriteLine($"Result is {obj}.");
            List<dynamic> list = conn.GetDynamicResultSet(sql: "SELECT *, 1 AS STOCK_ID FROM stock;");
            conn.Close();
            Console.WriteLine("Connection closed.");
            IDictionary<string, object> dict;
            Console.WriteLine("**************************");
            foreach (var item in list)
            {
                dict = item;

                foreach (var key in dict.Keys)
                {
                    Console.WriteLine($"{key} : {dict[key]}");
                }
                Console.WriteLine("---------------------------");
            }
            Console.WriteLine("**************************");
            Console.ReadKey();
        }
    }
}
