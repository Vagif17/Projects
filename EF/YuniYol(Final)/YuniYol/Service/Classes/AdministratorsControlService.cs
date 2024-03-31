using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.Service.Classes;

internal class AdministratorsControlService : IAdministratorsControlService
{

    public void MakeUser(Administrators SelectedAdministrator, MyDbContext context)
    {
        if (SelectedAdministrator == null || SelectedAdministrator.Name == null)
        {
            MessageBox.Show("Select Administrator");
            return;
        }

        Users tmp = new Users(SelectedAdministrator);

        context.Users.Add(tmp);
        context.Remove(SelectedAdministrator);
        context.SaveChanges();

    }
    public void DeleteAdministrator(Administrators SelectedAdministrator, MyDbContext context)
    {
        if (SelectedAdministrator == null || SelectedAdministrator.Name == null)
        {
            MessageBox.Show("Select Administrator");
            return ;
        }

        context.Remove(SelectedAdministrator);
        context.SaveChanges();

    }
}
