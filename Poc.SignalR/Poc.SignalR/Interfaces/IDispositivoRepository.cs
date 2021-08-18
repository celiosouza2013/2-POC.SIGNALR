using Poc.SignalR.Models;
using System.Collections.Generic;

namespace Poc.SignalR.Interfaces
{
    public interface IDispositivoRepository
    {
        Dispositivo Add(string connectionId, Dispositivo dispositivo);
        void Disconnect(string connectionId);
        List<Dispositivo> GetDispositivos();
        Dispositivo GetDispositivoByHash(string hash);
    }
}