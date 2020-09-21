using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        private ISpeechToText _speechRecongnitionInstance;

        public MainPage()
        {
            InitializeComponent();
            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                recon.Text = ex.Message;
            }


            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
                start.IsEnabled = true;
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });
        }

        private async void SpeechToTextFinalResultRecieved(string args)
        {
            recon.Text = args;
            await DisplayAlert("Gravação", "Transcrição do aúdio: " + args, "OK");
            if (analisaResultadoAPI(await analiseSentimento(args)))
                await DisplayAlert("Análise", "Existem indícios de comportamento agressivo" , "OK");
            else
                await DisplayAlert("Análise", "Não existem indícios de comportamento agressivo", "OK");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {            
            try
            {
                _speechRecongnitionInstance.StartSpeechToText();
            }
            catch (Exception ex)
            {
                recon.Text = ex.Message;
            }
        }

        static async Task<String> analiseSentimento(string texto)
        {
            // ... Use HttpClient.
            HttpClient client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("1575-12+AosVZ:tVmYCvtDx1BJ2sF6GEK6Wj1GQUdY1ulynqGkqQ9zr5BF");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var data = new { T = texto, SL = "PtBr", EM = "True" };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.gotit.ai/NLU/v1.4/Analyze", content);
            HttpContent responseContent = response.Content;

            // ... Check Status Code                                            
            //Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            string result = await responseContent.ReadAsStringAsync();

            return result;
        }

        bool analisaResultadoAPI(string resultado)
        {
            JObject JSON = JObject.Parse(resultado);
            string raiva = (string)JSON["emotions"]["anger"];
            string medo = (string)JSON["emotions"]["fear"];
            if (Convert.ToDouble(raiva) > 0.8)
                return true;
            if (Convert.ToDouble(medo) > 0.8)
                return true;
            return false;
        }

        
    }
}
