using Poc.SignalR.Models;
using System.Collections.Generic;

namespace Poc.SignalR.Interfaces
{
    public interface IMensagemRepository
    {
        Mensagem Add(Mensagem mensagem);
        void RemoverAposEnvio(int idMensagem);
        List<Mensagem> GetMensagem();
        //Mensagem GetMensagemByHash(string hash);
        List<Mensagem> GetMensagensByHash(string hash);
    }
}
