using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.Service.Classes;

internal class UsersControlService : IUsersControlService
{
    

    public void MakeAdministrator(Users SelectedUser, MyDbContext context)
    {
        if (SelectedUser == null || SelectedUser.Name == null)
        {
            MessageBox.Show("Select User");
            return;
        }

        Administrators tmp = new Administrators(SelectedUser);

        context.Administrators.Add(tmp);
        context.Remove(SelectedUser);
        context.SaveChanges();

    }
    public void DeleteUser(Users SelectedUser, MyDbContext context)
    {
        if (SelectedUser == null || SelectedUser.Name == null)
        {
            MessageBox.Show("Select User");
            return ;
        }


        context.Remove(SelectedUser);
        context.SaveChanges();

    }
}
