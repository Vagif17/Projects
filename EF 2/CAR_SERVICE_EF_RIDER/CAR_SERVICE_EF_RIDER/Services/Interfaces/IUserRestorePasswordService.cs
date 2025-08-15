using CAR_SERVICE_EF_RIDER.Models;

namespace CAR_SERVICE_EF_RIDER.Services.Interfaces;

public interface IUserRestorePasswordService
{
    public bool Check_Parameters(string Email);
    public bool Find_User(string Email);
    public string Send_VerifyCode(string Email);
    public void Update_Database(string NewPassword, string ConfirmPassword,string Email);
}