using DemoWASM.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
            string token = await JS.InvokeAsync<string>("localStorage.getItem", "token");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await Client.PostAsJsonAsync("Game", GameForm);
            Nav.NavigateTo("Exo3");
        }

        public async Task Return()
        {
            Nav.NavigateTo("Exo3");
        }
    }
}
