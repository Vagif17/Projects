using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Models;
using Storage = YuniYol.Models.Storage;

namespace YuniYol.Messages;

internal class AllStoragesMessage
{

    public List<Storage> storages { get; set; }

}
