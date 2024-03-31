using MyMonefy.Models;
using MyMonefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyMonefy.Services.Classes;

class BudgetService : IBudgetService
{
    public void ExpnesesSerialization(Expenses expenses)
    {
        using FileStream fs = new("ExpnesesData.json", FileMode.Append);
        using StreamWriter sw = new(fs);

        string json = JsonSerializer.Serialize(expenses);
        sw.WriteLine(json);
    }
}
