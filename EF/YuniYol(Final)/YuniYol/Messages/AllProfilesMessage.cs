using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuniYol.Models;

namespace YuniYol.Messages;

internal class AllProfilesMessage
{

    public List<Users> AllUsers { get; set; }
    public List<Administrators>  AllAdministrators { get; set; }


}
