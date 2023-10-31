using Sat.Recruitment.Api.Models;
using System.IO;

namespace Sat.Recruitment.Api.Interfaces
{
    public interface IUserService
    {
        public decimal GetMoneyByUserType(User user);
        public string NormalizeUserEmail(string email);
        public bool CheckDuplicatedUser(User user);
    }
}
