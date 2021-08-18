using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Poc.SignalR.Hubs;
using Poc.SignalR.Interfaces;
using Poc.SignalR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<MsgHub> _hub;
        private readonly IDispositivoRepository _dispositivoRepository;
        private readonly IMensagemRepository _mensagemRepository;

        public MessagesController(IDispositivoRepository dispositivoRepository,
                                  IMensagemRepository mensagemRepository,
                                  IHubContext<MsgHub> hub)
        {
            _dispositivoRepository = dispositivoRepository;
            _mensagemRepository = mensagemRepository;
            _hub = hub;
        }

        /// <summary>
        /// Método responsável por Salvar Mensagens e encaminhar pelo hub caso haja dispositivo conectado
        /// </summary>
        /// <param name="Mensagem">Este parâmetro é o objeto representando a mensagem</param>
        /// <returns></returns>
        // POST: api/Messages        
        [HttpPost]
        public async Task<ActionResult<Mensagem>> PostMensagem(Mensagem mensagem)
        {
            try
            {
                _mensagemRepository.Add(mensagem);

                //Verificar se já há  Dispositivo conectado e enviar mensagem
                var connectionId = _dispositivoRepository.GetDispositivoByHash(mensagem.hashDispositivo).ConnectionHost;

                if (connectionId != null && connectionId != "0")
                {
                    var messege = JsonConvert.SerializeObject(mensagem);
                    Task t = _hub.Clients.Client(connectionId).SendAsync("ReceiveMessege", messege);
                    if (t.IsCompletedSuccessfully)
                    {
                        _mensagemRepository.RemoverAposEnvio(mensagem.id);
                    }
                }
                return mensagem;
            }
            catch { throw; }
        }

        [HttpGet]
        [Route("dispositivos")]
        public async Task<List<Dispositivo>> DispositovosOnLine()
        {
            return _dispositivoRepository.GetDispositivos();
        }

        [HttpGet]
        [Route("mensagens")]
        public async Task<List<Mensagem>> GetMensagens()
        {
            return _mensagemRepository.GetMensagem();
        }

    }
}