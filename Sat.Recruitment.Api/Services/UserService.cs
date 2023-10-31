using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    { 
        public UserService() { }

        public bool CheckDuplicatedUser(User user)
        {
            var response = false;
            var reader = ReadUsersFromFile();
            List<User> users = new List<User>();
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var newUser = new User(
                    line.Split(',')[0].ToString(),
                    line.Split(',')[1].ToString(),
                    line.Split(',')[3].ToString(),
                    line.Split(',')[2].ToString(),
                    (UserType)Enum.Parse(typeof(UserType), line.Split(',')[4].ToString(), true),
                    line.Split(',')[5].ToString()
                );
                users.Add(newUser);
            }
            reader.Close();
            foreach (var item in users)
            {
                if (user.Email == item.Email || user.Phone == item.Phone)
                {
                    response = true;
                }
                else if (user.Name == item.Name && user.Address == item.Address)
                {
                    response = true;
                    throw new Exception("User is duplicated");
                }
            }
            return response;
        }

        public decimal GetMoneyByUserType(User user)
        {
            decimal response = 0;
            if (user.UserType is UserType.Normal)
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = user.Money * percentage;
                    response = user.Money + gif;
                }
                else if (user.Money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = user.Money * percentage;
                    response = user.Money + gif;
                }
            }
            else if (user.UserType is UserType.SuperUser && user.Money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = user.Money * percentage;
                response = user.Money + gif;
            }
            else if (user.UserType is UserType.Premium && user.Money > 100)
            {
                var gif = user.Money * 2;
                response = user.Money + gif;
            }
            return response;
        }

        public string NormalizeUserEmail(string email)
        {
            //Normalize email
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
