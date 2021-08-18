using Poc.SignalR.Context;
using Poc.SignalR.Interfaces;
using Poc.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poc.SignalR.Repositories
{
    public class MensagemRepository : IMensagemRepository
    {
        private readonly DataContext _context;
        public MensagemRepository(DataContext context) => _context = context;

        public Mensagem Add(Mensagem mensagem)
        {
            try
            {
                _context.Mensagens.Add(mensagem);
                _context.SaveChanges();
                return mensagem;
            }
            catch { throw; }
        }

        public List<Mensagem> GetMensagem()
        {
            try
            {
                return _context.Mensagens.ToList();
            }
            catch { throw; }
        }

        public List<Mensagem> GetMensagensByHash(string hash)
        {
            var ModelMensagem = new Mensagem();
            ModelMensagem.hashDispositivo = hash;
            try
            {
                return _context.Mensagens.Where<Mensagem>(obj => obj.hashDispositivo.Equals(hash)).OrderBy(msg => msg.id).ToList();
            }
            catch { throw; }
        }

        public void RemoverAposEnvio(int idMensagem)
        {
            try
            {
                _context.Mensagens.Remove(_context.Mensagens.FirstOrDefault(obj => obj.id.Equals(idMensagem)));
                _context.SaveChanges();
            }
            catch { throw; }
        }
    }
}