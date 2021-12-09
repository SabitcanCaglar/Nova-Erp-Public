using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaErp.UI.Hubs
{
    public class ÜrünHub:Hub
    {
        public async Task ÜrünTabloReload(string message)
        {

            await Clients.All.SendAsync("ürüneklesilgüncelle", message);

        }

    }
}
