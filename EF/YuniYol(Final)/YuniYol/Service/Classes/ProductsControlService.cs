using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YuniYol.Models;
using YuniYol.Service.Interfaces;

namespace YuniYol.Service.Classes;

internal class ProductsControlService : IProductsControlService
{
    public void CreateProduct(float Price, string Title, string Description, DateTime ExpirationDate, DateTime DateOfIssue)
    {
        if (Price == null || Price == 0 || Title == null || Title == "" || Description == null || Description == "" || ExpirationDate == null || DateOfIssue == null)
        {
            MessageBox.Show("Wrong information!Check it ");
            return;
        }

        Products products = new Products { Name = Title, Price = Price, Description = Description, DateOfIssue = DateOfIssue, ExpirationDate = ExpirationDate };

        MyDbContext myDbContext = new MyDbContext();
        myDbContext.Products.Add(products);
        myDbContext.SaveChanges();


    }

    public void DeleteProduct(Products SelectedProduct, MyDbContext myDbContext)
    {
        if (SelectedProduct == null || SelectedProduct.Name == null)
        {
            MessageBox.Show("Select product");
            return ;
        }


        myDbContext.Remove(SelectedProduct);
        myDbContext.SaveChanges();
    }
}
