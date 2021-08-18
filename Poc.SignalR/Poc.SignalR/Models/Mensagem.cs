using System;
using System.ComponentModel.DataAnnotations;

namespace Poc.SignalR.Models
{
    public class Mensagem
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string hashDispositivo { get; set; }
        public string tokenDispositivo { get; set; }
        public string Titulo { get; set; }
        public string TextoMensagem { get; set; }
        public int CodigoCriticidade { get; set; }
        public string DescricaoCriticidade { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataHoraInicioVigencia { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataHoraFinalVigencia { get; set; }
        public int SequenciaInteresse { get; set; }
        public int IdAreaRisco { get; set; }
        public string Result { get; set; }

        //public string MensagemALerta { get; set; }
        //public string ClientConectionId { get; set; }
        //public Int64 DispositivoId { get; set; }
        //public Dispositivo Dispositivo { get; set; }        

    }
}