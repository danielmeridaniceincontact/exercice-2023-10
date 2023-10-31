using Sat.Recruitment.Api.Interfaces;

namespace Sat.Recruitment.Api.Services
{
    public class ErrorService : IErrorService
    {
        public ErrorService() { }

        public void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            errors = string.Empty;
            if (string.IsNullOrEmpty(name))
                //Validate if Name is null
                errors = "The name is required";
            if (string.IsNullOrEmpty(email))
                //Validate if Email is null
                errors += " The email is required";
            if (string.IsNullOrEmpty(address))
                //Validate if Address is null
                errors += " The address is required";
            if (string.IsNullOrEmpty(phone))
                //Validate if Phone is null
                errors += " The phone is required";
        }
    }
}
