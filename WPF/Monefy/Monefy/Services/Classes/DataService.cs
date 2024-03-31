using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

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

  

}

