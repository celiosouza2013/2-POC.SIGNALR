using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.SignalR
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connection = new HubConnectionBuilder()

                                .WithUrl("https://localhost:44332/streaminghub?Hash=celio123", options =>
                                {
                                    options.UseDefaultCredentials = true;
                                })
                                .WithAutomaticReconnect()
                                .Build();

                connection.On<string>("ReceiveMessege", (message) =>
                {
                    Console.WriteLine(message);
                });

                connection.Closed += (error) =>
                {
                    Console.WriteLine("Conexão fechada.!");
                    return Task.CompletedTask;
                };

                connection.Reconnecting += (error) =>
                {
                    Console.WriteLine("Reconectando.!");
                    return Task.CompletedTask;
                };

                connection.Reconnected += (error) =>
                {
                    Console.WriteLine("Conexão reconectada.!");
                    return Task.CompletedTask;
                };

                connection.StartAsync().Wait();
                Console.WriteLine("Conectado! ConnectionId: " + connection.ConnectionId);
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao tentar conectar!");
            }
            

            #region Utilize para testar envio de mensagem a API Signalr
            
            //Opção Utilize Swagger na API
            //while (true)
            //{

            //    try
            //    {
            //        var _urlWebervice = "https://localhost:44332/api/Messages";
            //        HttpClient _httpClient = new HttpClient();
            //        _httpClient.BaseAddress = new Uri(_urlWebervice);
            //        _httpClient.DefaultRequestHeaders.Accept.Clear();
            //        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        var data = new JObject();
            //        //data["id"] = 0;//++contador;
            //        data["hashDispositivo"] = "celio123";
            //        data["tokenDispositivo"] = "token";
            //        data["titulo"] = "titulo";
            //        data["textoMensagem"] = "texto da mensagem";
            //        data["codigoCriticidade"] = 1;
            //        data["descricaoCriticidade"] = "Crítico";
            //        data["dataHoraInicioVigencia"] = "2020-09-30T02:07:27.067Z";
            //        data["dataHoraFinalVigencia"] = "2020-09-30T02:07:27.067Z";
            //        data["sequenciaInteresse"] = 2;
            //        data["idAreaRisco"] = 3;
            //        data["result"] = "ok";
            //        string debug = JsonConvert.SerializeObject(data);

            //        var json = new StringContent(JsonConvert.SerializeObject(data),
            //                                         Encoding.UTF8, "application/json");
            //        var response = _httpClient.PostAsync("", json);
            //        var result = response.Result.IsSuccessStatusCode;

            //        //return result;
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }

            //    Thread.Sleep(1000);
            //}
            //Console.ReadKey();
        }
            #endregion
    }
}


