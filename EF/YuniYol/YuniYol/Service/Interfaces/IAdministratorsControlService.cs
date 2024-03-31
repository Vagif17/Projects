using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Models;

namespace YuniYol.Service.Interfaces;

internal interface IAdministratorsControlService
{
    public void MakeUser(Administrators SelectedAdministrator,MyDbContext context);
    public void DeleteAdministrator(Administrators SelectedAdministrator,MyDbContext context);

}
