using Sat.Recruitment.Api.Enums;
using System.Data.SqlTypes;
using System.Net;
using System.Security.Policy;
using System.Xml.Linq;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        private string _name;
        private string _email;
        private string _address;
        private string _phone;
        private UserType _userType;
        private decimal _money;

        public User(string name, string email, string address, string phone, UserType userType, string money)
        {
            _name = name;
            _email = email;
            _address = address;
            _phone = phone;
            _userType = userType;
            _money = decimal.Parse(money);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public UserType UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }
        public decimal Money
        {
            get { return _money; }
            set { _money = value; }
        }
    }
}
