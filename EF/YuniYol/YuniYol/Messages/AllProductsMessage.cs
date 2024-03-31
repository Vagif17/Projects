using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Models;

namespace YuniYol.Messages;

 class AllProductsMessage
{
    public List<Products> Products { get; set; }
}
