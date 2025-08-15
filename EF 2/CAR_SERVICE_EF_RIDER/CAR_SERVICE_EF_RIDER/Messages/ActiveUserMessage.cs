using CAR_SERVICE_EF_RIDER.Interfaces;

namespace CAR_SERVICE_EF_RIDER.Messages;

public class ActiveUserMessage
{
    public IPerson Person { get; set; }
}