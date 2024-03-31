using MyMonefy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonefy.Services.Interfaces;

interface IDataService
{
    public void SendData(Object[]? data);
    public void SendExpenses(Expenses expenses);
    public void SendActiveUser(string name);

}
