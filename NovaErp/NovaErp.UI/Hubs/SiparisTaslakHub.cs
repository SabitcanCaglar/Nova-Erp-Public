using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Hubs
{
    public class SiparisTaslakHub:Hub
    {

        public async Task SiparisTaslakTabloReload(string message)
        {

            await Clients.All.SendAsync("siparistaslakeklesilgüncelle", message);
            


        }
       
    }
}
