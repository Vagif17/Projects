using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Models;

namespace YuniYol.Service.Interfaces;

internal interface IProductsControlService
{
    public void CreateProduct(float Price, string Title, string Description, DateTime ExpirationDate, DateTime DateOfIssue);
    public void DeleteProduct(Products SelectedProduct,MyDbContext myDbContext);


}
