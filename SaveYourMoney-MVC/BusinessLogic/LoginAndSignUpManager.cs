//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using SaveYourMoney_MVC.Entities;
//using SaveYourMoney_MVC.Repositories;

//namespace SaveYourMoney_MVC.BusinessLogic
//{

//    public class LoginAndSignUpManager 
//    {
//        private UserLoginDetails userLoginDetails;

//        private readonly SaveYourMoneyDbContext SaveYourMoneyDbContext;

//        public LoginAndSignUpManager(SaveYourMoneyDbContext saveYourMoneyDbContext)
//        {
//            SaveYourMoneyDbContext = saveYourMoneyDbContext ?? throw new ArgumentNullException(nameof(saveYourMoneyDbContext));
//        }

//        public bool GetUserLoginDetails(string userEnteredUsername)
//        {
//            bool DoesUserExist = false;
           
//            try
//            {
//                //userLoginDetails = SaveYourMoneyDbContext.GetUserLoginDetails(userEnteredUsername);
//                DoesUserExist = true;
//            }
//            catch (Exception ex)
//            {
//                // Handle exceptions
//                Console.WriteLine($"Error fetching user login details: {ex.Message}");
//            }

//            return DoesUserExist;
//        }
//    }
//}
