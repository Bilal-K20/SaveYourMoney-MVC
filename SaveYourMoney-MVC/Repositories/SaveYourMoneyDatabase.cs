using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SaveYourMoney_MVC.Entities;
using SaveYourMoney_MVC.Models;

namespace SaveYourMoney_MVC.Repositories
{
	public class SaveYourMoneyDatabase : ISaveYourMoneyDatabase
	{
		private readonly IConfiguration _configuration;
		private  Database SaveYourMoneySqlDatabase; 

		public SaveYourMoneyDatabase(IConfiguration configuration, Database database)
		{
			_configuration = configuration;
			SaveYourMoneySqlDatabase = database;
		}


        public UserLoginDetails GetUserLoginDetails(string userEnteredUsername)
        {
            // I guess i could use hashMaps instead of lists <key,value> <username, password> 
            UserLoginDetails loginDetails = new UserLoginDetails();
            string hashedPassword;
            string username;

            using (DbConnection connection = SaveYourMoneySqlDatabase.CreateConnection())
            {
                connection.Open();

                using (DbCommand command = SaveYourMoneySqlDatabase.GetStoredProcCommand("dbo.GetCustomerByUsername"))
                {
                    command.Connection = connection;
                    SaveYourMoneySqlDatabase.AddInParameter(command, "@Username", DbType.String, userEnteredUsername);
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        {
                            username = Convert.ToString(reader["Username"]);
                            hashedPassword = Convert.ToString(reader["Password"]);
                            // do we need to return password?

                            loginDetails.Username = username;
                            loginDetails.HashedPassword = hashedPassword;
                        };
                    }

                }

                return loginDetails;
            }
        }
    }
}

