using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.Service.Classes;

internal class StorageControlService : IStorageControlService
{
    public void DeleteStorage(Storage SelectedStorage, MyDbContext context)
    {
        if (SelectedStorage == null || SelectedStorage.UserId == null)
        {
            MessageBox.Show("Select Storage");
            return;
        }

        context.Storages.Remove(SelectedStorage);
        context.SaveChanges();

    }

    public void InTheStorageFunc(Storage SelectedStorage, MyDbContext dbContext)
    {
        if (SelectedStorage != null)
        {



            Storage storage = new(SelectedStorage);
            storage.Id = 0;



            if (storage.TrackinId == "In The Storage")
            {
                MessageBox.Show("Already in the storage!");
                return;
            }
            if (storage.TrackinId == "Ordered")
            {

                if (SelectedStorage == null || SelectedStorage.UserId == null)
                {
                    MessageBox.Show("Select Storage");
                    return;
                }

                dbContext.Storages.Remove(SelectedStorage);
                dbContext.SaveChanges();


                storage.TrackinId = "In The Storage";

                dbContext.Storages.Add(storage);
                dbContext.SaveChanges();


            }

            else
            {
                MessageBox.Show("Tracking ID is not Ordered");
            }
        }
        else
        {
            MessageBox.Show("Choose Storage!");
        }
    }


    public void OrderedFunc(Storage SelectedStorage, MyDbContext dbContext)
    {
        if (SelectedStorage != null)
        {



            Storage storage = new(SelectedStorage);
            storage.Id = 0;



            if (storage.TrackinId == "Ordered")
            {
                MessageBox.Show("Already Ordered!");
                return;
            }
            if (storage.TrackinId == "")
            {

                if (SelectedStorage == null || SelectedStorage.UserId == null)
                {
                    MessageBox.Show("Select Storage");
                    return;
                }

                dbContext.Storages.Remove(SelectedStorage);
                dbContext.SaveChanges();


                storage.TrackinId = "Ordered";

                dbContext.Storages.Add(storage);
                dbContext.SaveChanges();


            }

            else
            {
                MessageBox.Show("Tracking ID is not empty");
            }
        }
        else
        {
            MessageBox.Show("Choose Storage!");
        }
    }

    public void SendedFunc(Storage SelectedStorage, MyDbContext dbContext)
    {
        if (SelectedStorage != null)
        {



            Storage storage = new(SelectedStorage);
            storage.Id = 0;



            if (storage.TrackinId == "Sended")
            {
                MessageBox.Show("Already Sended!");
                return;
            }
            if (storage.TrackinId == "In The Storage")
            {

                if (SelectedStorage == null || SelectedStorage.UserId == null)
                {
                    MessageBox.Show("Select Storage");
                    return;
                }

                dbContext.Storages.Remove(SelectedStorage);
                dbContext.SaveChanges();


                storage.TrackinId = "Sended";

                dbContext.Storages.Add(storage);
                dbContext.SaveChanges();


            }

            else
            {
                MessageBox.Show("Tracking ID is not In The Storage");
            }
        }
        else
        {
            MessageBox.Show("Choose Storage!");
        }
    }
    public void OnTheBorderFunc(Storage SelectedStorage, MyDbContext dbContext)
    {
        if (SelectedStorage != null)
        {



            Storage storage = new(SelectedStorage);
            storage.Id = 0;



            if (storage.TrackinId == "On The Border")
            {
                MessageBox.Show("Already On The Border!");
                return;
            }
            if (storage.TrackinId == "Sended")
            {

                if (SelectedStorage == null || SelectedStorage.UserId == null)
                {
                    MessageBox.Show("Select Storage");
                    return;
                }

                dbContext.Storages.Remove(SelectedStorage);
                dbContext.SaveChanges();


                storage.TrackinId = "On The Border";

                dbContext.Storages.Add(storage);
                dbContext.SaveChanges();



            }

            else
            {
                MessageBox.Show("Tracking ID is not Sended");
            }
        }
        else
        {
            MessageBox.Show("Choose Storage!");
        }
    }

    public void AtThePostOffice(Storage SelectedStorage, MyDbContext dbContext)
    {
        if (SelectedStorage != null)
        {



            Storage storage = new(SelectedStorage);
            storage.Id = 0;



            if (storage.TrackinId == "At the post office")
            {
                MessageBox.Show("Already At the post office!");
                return;
            }
            if (storage.TrackinId == "On The Border")
            {

                if (SelectedStorage == null || SelectedStorage.UserId == null)
                {
                    MessageBox.Show("Select Storage");
                    return;
                }

                dbContext.Storages.Remove(SelectedStorage);
                dbContext.SaveChanges();


                storage.TrackinId = "At the post office";

                dbContext.Storages.Add(storage);
                dbContext.SaveChanges();


            }

            else
            {
                MessageBox.Show("Tracking ID is not On The Border");
            }
        }
        else
        {
            MessageBox.Show("Choose Storage!");
        }

    }
}
