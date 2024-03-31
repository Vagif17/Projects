using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Models;

namespace YuniYol.Service.Interfaces;

internal interface IUsersControlService
{
    public void MakeAdministrator(Users SelectedUser, MyDbContext context);
    public void DeleteUser(Users SelectedUser, MyDbContext context);
    
    
}
