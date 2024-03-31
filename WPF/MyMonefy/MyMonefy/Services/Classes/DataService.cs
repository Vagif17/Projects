using GalaSoft.MvvmLight.Messaging;
using MyMonefy.Messages;
using MyMonefy.Models;
using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonefy.Services.Classes;

class DataService : IDataService
{
    private readonly IMessenger _messenger;

    public DataService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void SendData(object[]? data = null)

    {
        if (data != null)
            _messenger.Send(new DataMessage() { Data = data });
        else
            throw new NullReferenceException("Data is null");
    }

    public void SendExpenses(Expenses expenses)
    {
        if (expenses != null)
        {
            _messenger.Send(new ExpensesMessage() {Expenses = expenses });
        }
    }

    public void SendActiveUser(string name)
    {
        if (name != null)
        {
            _messenger.Send(new ActiveUserMessage() { Name = name });
        }
    }

}

