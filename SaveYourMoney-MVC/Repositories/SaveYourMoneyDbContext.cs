using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SaveYourMoney_MVC.Entities;
using SaveYourMoney_MVC.Models;

using Microsoft.EntityFrameworkCore;
using SaveYourMoney_MVC.Entities;

namespace SaveYourMoney_MVC.Repositories
{
    public class SaveYourMoneyDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=SaveYourMoney.db");
        }
    }
}







//namespace SaveYourMoney_MVC.Repositories
//{
//    public class SaveYourMoneyDbContext : DbContext
//    {
//        private readonly IConfiguration Configuration;

//        public SaveYourMoneyDbContext(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }
//        public DbSet<Customer> Customers { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder options)
//        {
//            // connect to sql server with connection string from app settings
//            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

//        }

//        public UserLoginDetails GetUserLoginDetails(string userEnteredUsername)

//        {
//            UserLoginDetails loginDetails = new UserLoginDetails();

//            try
//            {
//                using (var connection = Database.GetDbConnection())
//                {
//                    connection.Open();

//                    using (var command = connection.CreateCommand())
//                    {
//                        command.CommandText = "dbo.GetCustomerByUsername";
//                        command.CommandType = CommandType.StoredProcedure;

//                        var parameter = command.CreateParameter();
//                        parameter.ParameterName = "@Username";
//                        parameter.DbType = DbType.String;
//                        parameter.Value = userEnteredUsername;
//                        command.Parameters.Add(parameter);

//                        using (var reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                loginDetails.Username = Convert.ToString(reader["Username"]);
//                                loginDetails.HashedPassword = Convert.ToString(reader["Password"]);
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle exceptions
//                Console.WriteLine($"Error fetching user login details: {ex.Message}");
//                var message = ex.Message;
//                var stackTrace = ex.StackTrace;
//            }

//            return loginDetails;
//        }

//    }
//}

