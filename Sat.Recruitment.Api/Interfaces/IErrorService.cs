namespace Sat.Recruitment.Api.Interfaces
{
    public interface IErrorService
    {
        public void ValidateErrors(string name, string email, string address, string phone, ref string errors);
    }
}
