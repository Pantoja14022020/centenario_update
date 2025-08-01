using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Hubs
{
    public class TurnoHub: Hub
    {
        public async Task EnviarTurno(string turno, string mesa)
        {
            await Clients.All.SendAsync("RecibirTurno", turno, mesa);
        }
    }
}
