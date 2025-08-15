using CAR_SERVICE_EF_RIDER.Interfaces;

namespace CAR_SERVICE_EF_RIDER.Services.Interfaces;

public interface IMessagesService
{
    public void SendActiveUser(IPerson User);
    public void SendVerifyCode(string VerifyCode);
    public void SentRestoreEmail(string Email);
    public void UpdateInfoMessage();
}