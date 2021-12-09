using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Hubs
{
    public class MusteriHub:Hub
    {
        public async Task MusteriTabloReload(string message)
        {
            
            await Clients.All.SendAsync("musterieklesilgüncelle", message);
            
        }

    }
}
