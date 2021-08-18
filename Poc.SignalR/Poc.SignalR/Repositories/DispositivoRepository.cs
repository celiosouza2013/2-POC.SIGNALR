using Poc.SignalR.Context;
using Poc.SignalR.Interfaces;
using Poc.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poc.SignalR.Repositories
{
    public class DispositivoRepository : IDispositivoRepository
    {
        private readonly DataContext _context;
        public DispositivoRepository(DataContext context) => _context = context;

        public Dispositivo Add(string connectionHost, Dispositivo dispositivo)
        {
            try
            {
                var logged = _context.Dispositivos.Where(obj => obj.ConnectionHost.Equals(connectionHost)).FirstOrDefault();
                if (logged is null)
                {
                    dispositivo.ConnectionHost = connectionHost;
                    _context.Dispositivos.Add(dispositivo);
                    _context.SaveChanges();
                    return dispositivo;
                }
                return logged;
            }
            catch { throw; }
        }

        public void Disconnect(string connectionHost)
        {
            try
            {
                var dispositivo = _context.Dispositivos.FirstOrDefault(obj => obj.ConnectionHost.Equals(connectionHost));
                var lstDispositivo = _context.Dispositivos.Where(obj => obj.HashDispositivo.Equals(dispositivo.HashDispositivo));
                foreach (Dispositivo disp in lstDispositivo)
                {
                    try
                    {
                        _context.Dispositivos.Remove(disp);
                    }
                    catch { } // Se der erro em uma exclusão, não perdemos todos.
                }
                _context.SaveChanges();
            }
            catch { throw; }
        }

        public List<Dispositivo> GetDispositivos()
        {
            try
            {
                return _context.Dispositivos.OrderBy(disp => disp.Id).ToList();
            }
            catch { throw; }
        }

        public Dispositivo GetDispositivoByHash(string hash)
        {
            try
            {
                Dispositivo _dispositivo = _context.Dispositivos.FirstOrDefault(obj => obj.HashDispositivo.Equals(hash));
                return _dispositivo != null ? _dispositivo : new Dispositivo() { HashDispositivo = "0" };
            }
            catch { throw; }
        }
    }
}