using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace KeyMaster.Models
{
    public class dbConnect : DbContext
    {
        string mysqlConn = "server=127.0.0.1;user=root;database=keymaster; password=";
        public virtual DbSet<UserModel> Login { get; set; }
       

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlConn);
            try
            {
                mySqlConnection.Open();
                Console.WriteLine("połączyłem sie z bazą");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error" + ex);
            }
            finally { mySqlConnection.Close(); }
            /*
            optionsBuilder
                .UseMySql("Server=127.0.0.1;port=3306;Database=KeyMaster;user=root")
                .UseLoggerFactory(LoggerFactory.Create(b =>
                .AddConsole()
                .AddFilter(level => level => LogLevel.Information)))
                .EnableSe
                */
        }
    }
}
