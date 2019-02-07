using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Dexter.Factory;
using Dexter.QueryExtensions;
using System.Configuration;
using System.Diagnostics;

namespace Dexter.EntityTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var personFile = new PersonalFile();
            // var personFile = new TransactionLog();
            personFile.Id = 10;
            personFile.FileName = "SomeFile";
            personFile.FileContent = new byte[] { 12, 34, 56, 78, 74, 35, 10 };
            personFile.CreatedBy = 1;
            personFile.CreationDate = DateTime.Now;
            personFile.IsActive = true;

            IDbConnection sqlConn = ConnectionFactory.Instance.GetConnection(ConfigurationManager.AppSettings["connTypeName"]);//"sql");
            sqlConn.ConnectionString = ConfigurationManager.AppSettings["connString"];

            /*
            object o = null, o2 = null;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            o = DbConnectionExecutionExtensions.Insert(sqlConn, personFile);
            sw.Stop();
            Console.WriteLine(o);
            Console.WriteLine($"Geçen Süre : {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Geçen Süre : {sw.ElapsedTicks} Ticks");

            sw.Reset();
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            o2 = DbConnectionExecutionExtensions.Insert(sqlConn, personFile);
            sw2.Stop();
            Console.WriteLine(o2);
            Console.WriteLine($"Geçen Süre : {sw2.ElapsedMilliseconds} ms");
            Console.WriteLine($"Geçen Süre : {sw2.ElapsedTicks} Ticks");
            */
            /*
            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            object o3 = EntityCrudFactory.Instance.Insert(sqlConn, personFile);
            sw3.Stop();
            Console.WriteLine(o3);
            Console.WriteLine($"Geçen Süre : {sw3.ElapsedMilliseconds} ms");
            Console.WriteLine($"Geçen Süre : {sw3.ElapsedTicks} Ticks");

            Stopwatch sw4 = new Stopwatch();
            sw4.Start();
            object o4 = EntityCrudFactory.Instance.Insert(sqlConn, personFile);
            sw4.Stop();
            Console.WriteLine(o4);
            Console.WriteLine($"Geçen Süre : {sw4.ElapsedMilliseconds} ms");
            Console.WriteLine($"Geçen Süre : {sw4.ElapsedTicks} Ticks");
            */

            int count = 10000;

            try
            {
                string s = ConfigurationManager.AppSettings["insertKeyCount"];
                int.TryParse(s, out count);

                if (count < 100)
                    count = 100;

                if (count > 1000000)
                    count = 1000000;
            }
            catch (Exception)
            {
            }
            Stopwatch sw;
            object o;
            long ticks = 0L;
            long msec = 0L;
            for (int counter = 0; counter < count; counter++)
            {
                sw = null;
                sw = new Stopwatch();
                sw.Start();
                o = EntityCrudFactory.Instance.Update(sqlConn, personFile);
                sw.Stop();
                Console.WriteLine(o);
                Console.WriteLine($"Geçen Süre : {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"Geçen Süre : {sw.ElapsedTicks} Ticks");
                Console.WriteLine("-------------------------------------------------");
                msec += sw.ElapsedMilliseconds;
                ticks += sw.ElapsedTicks;
            }
            Console.WriteLine($"Toplam Kayıt Sayısı : {count}");
            Console.WriteLine($"Toplam Süre : {msec} ms, Ortalama: {(double)msec / count}");
            Console.WriteLine($"Toplam Süre Tik : {ticks} tick, Ortalama: {(double)ticks / count}");

            Console.ReadKey();
        }
    }

    [Table("LogEntry")]
    public partial class LogEntry
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime LogTime { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(50)]
        public string ErrorCode { get; set; }

        [StringLength(100)]
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public virtual User User { get; set; }
    }

    public partial class PersonalFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; }

    }


    [Table("TransactionLog")]
    public partial class TransactionLog
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime LogTime { get; set; }

        [Required]
        [StringLength(50)]
        public string TableName { get; set; }

        public int? EntityId { get; set; }

        [StringLength(50)]
        public string TransactionType { get; set; }

        public virtual User User { get; set; }
    }


    public partial class User
    {
        public User()
        {
            LogEntries = new HashSet<LogEntry>();
            PersonalFiles = new HashSet<PersonalFile>();
            TransactionLogs = new HashSet<TransactionLog>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Pass { get; set; }

        public int UserType { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<LogEntry> LogEntries { get; set; }

        public virtual ICollection<PersonalFile> PersonalFiles { get; set; }

        public virtual ICollection<TransactionLog> TransactionLogs { get; set; }

        public virtual UserType UserType1 { get; set; }
    }

    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

}
