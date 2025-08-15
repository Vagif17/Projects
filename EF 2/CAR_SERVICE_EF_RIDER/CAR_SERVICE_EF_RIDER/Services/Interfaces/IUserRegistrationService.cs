namespace CAR_SERVICE_EF_RIDER.Services.Interfaces;

public interface IUserRegistrationService
{
    public bool Check_Parameters(string email,string name,string password,string passwordconfirm);
    public void Register_User(string email, string name, string password);
}