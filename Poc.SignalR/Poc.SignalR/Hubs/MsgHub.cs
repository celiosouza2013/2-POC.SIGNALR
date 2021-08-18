using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Poc.SignalR.Interfaces;
using Poc.SignalR.Models;
using System;
using System.Threading.Tasks;

namespace Poc.SignalR.Hubs
{
    public class MsgHub : Hub
    {
        private readonly IDispositivoRepository _dispositivoRepository;
        private readonly IMensagemRepository _mensagemRepository;

        public MsgHub(IDispositivoRepository dispositivoRepository,
                      IMensagemRepository mensagemRepository)
        {
            _dispositivoRepository = dispositivoRepository;
            _mensagemRepository = mensagemRepository;
        }

        /// <summary>
        /// Override para inserir cada Dispositivo vinculando Conexão com Hash e verificar se há alguma mensagem salva para cada nova conexão de dispositivo 
        /// </summary>
        /// <returns></returns>        
        public override Task OnConnectedAsync()
        {
            try
            {
                var httpContext = Context.GetHttpContext();
                Dispositivo dispositivo = new Dispositivo();
                if (httpContext != null)
                {
                    dispositivo.HashDispositivo = httpContext.Request.Query["Hash"];
                    dispositivo = _dispositivoRepository.Add(Context.ConnectionId, dispositivo);

                    var mensagens = _mensagemRepository.GetMensagensByHash(dispositivo.HashDispositivo);
                    var jsonResult = "";

                    mensagens.ForEach(msg =>
                    {
                        jsonResult = JsonConvert.SerializeObject(msg);
                        Task t = Clients.Client(dispositivo.ConnectionHost).SendAsync("ReceiveMessege", jsonResult);
                        if (t.IsCompletedSuccessfully)
                        {
                            _mensagemRepository.RemoverAposEnvio(msg.id);
                        }
                    });
                }
                else
                {
                    throw new Exception("contexto atual não existente.");
                }
                return base.OnConnectedAsync();
            }
            catch { throw; }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                _dispositivoRepository.Disconnect(Context.ConnectionId);
                return base.OnDisconnectedAsync(exception);
            }
            catch { throw; }
        }
    }
}