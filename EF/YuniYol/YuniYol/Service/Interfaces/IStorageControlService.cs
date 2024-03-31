using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Models;

namespace YuniYol.Service.Interfaces;

internal interface IStorageControlService
{
    public void DeleteStorage(Storage SelectedStorage,MyDbContext dbContext);
    public void OrderedFunc(Storage SelectedStorage,MyDbContext dbContext);
    public void InTheStorageFunc(Storage SelectedStorage,MyDbContext dbContext);
    public void SendedFunc(Storage SelectedStorage,MyDbContext dbContext);
    public void OnTheBorderFunc(Storage SelectedStorage,MyDbContext dbContext);
    public void AtThePostOffice(Storage SelectedStorage,MyDbContext dbContext);
}
