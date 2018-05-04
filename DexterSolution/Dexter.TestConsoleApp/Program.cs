using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dexter;
using Dexter.Factory;
using Dexter.Extensions;

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

            IDbConnection conn = ConnectionFactory.Instance.GetConnection(sConnType);//"mysql");
            conn.ConnectionString = sConnStr;

            conn.Open();
            Console.WriteLine("Connection opened.");
            object obj = conn.ExecuteScalar("SELECT COUNT(1) FROM stock;");
            Console.WriteLine($"Result is {obj}.");
            List<dynamic> list = conn.GetDynamicResultSet(sql: "SELECT *, 1 AS STOCK_ID FROM stock;");
            conn.Close();
            Console.WriteLine("Connection closed.");
            //List<dynamic> list = conn.ExecuteReader("SELECT *, 1 AS STOCK_ID FROM stock;").GetDbReaderResultV2();
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
