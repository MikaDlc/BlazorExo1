using DemoWASM.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DemoWASM.Pages.Exercices.Games
{
    public partial class GameAdd
    {
        [Inject]
        public NavigationManager Nav { get; set; }
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }
        public GamePost GameForm { get; set; }

        public GameAdd()
        {
            GameForm = new GamePost();
        }

        public async Task Add()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7050/api/");
            string token = await JS.InvokeAsync<string>("localStorage.getItem", "token");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(GameForm);
            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpResponseMessage m = await Client.PostAsJsonAsync("Game", GameForm);
            HttpResponseMessage m = await Client.PostAsync("game", c);
            if(!m.IsSuccessStatusCode) Console.WriteLine(m.ReasonPhrase + " - " + m.StatusCode);
            Nav.NavigateTo("Exo3");
        }

        public async Task Return()
        {
            Nav.NavigateTo("Exo3");
        }
    }
}
