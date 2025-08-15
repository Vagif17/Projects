using CAR_SERVICE_EF_RIDER.Interfaces;

namespace CAR_SERVICE_EF_RIDER.Services.Interfaces;

public interface ILogInService
{
    public IPerson LogInUser(string email,string password);
    public IPerson LogInAdmin(string email,string password);

}