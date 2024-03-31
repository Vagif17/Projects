using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Interfaces;
using YuniYol.Models;

namespace YuniYol.Messages;

 class ActiveUserMessage
{
    
    public IPerson User { get; set; }
    public List<Products> Products { get; set; }

}
