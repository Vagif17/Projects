using CAR_SERVICE_EF_RIDER.Services.Interfaces;
using System;
using Serilog;

namespace CAR_SERVICE_EF_RIDER.Services.Classes;

public class LoggerService : ILoggerService
{

    public LoggerService()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() 
            .WriteTo.File(
                path: "C:\\Users\\User\\RiderProjects\\CAR_SERVICE_EF_RIDER\\CAR_SERVICE_EF_RIDER\\Logs.txt", 
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();
    }
    
    public void CreateLog(string message,int type)
    {
        switch (type)
        {
            case 1:
                Log.Information(message);
                break
                ;
            case 2:
                Log.Error(message);
                break;
            case 3:
                Log.Debug(message);
                break;
        }
    }
}