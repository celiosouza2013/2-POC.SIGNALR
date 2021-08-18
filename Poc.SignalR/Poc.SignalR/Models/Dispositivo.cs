using System;

namespace Poc.SignalR.Models
{
    public class Dispositivo
    {
        public Dispositivo()
        {
            this.dtConnection = DateTime.Now;
        }
        public Int64 Id { get; set; }
        public string HashDispositivo { get; set; }
        public string DispositivoId { get; set; }
        public DateTime dtConnection { get; set; }
        public string ConnectionHost { get; set; }
        public string Result { get; set; }
    }
}