using CAR_SERVICE_EF_RIDER.Models;

namespace CAR_SERVICE_EF_RIDER.Messages;

public class UpdateInfoMessage
{
    public MyDbContext DbContext { get; set; }
}