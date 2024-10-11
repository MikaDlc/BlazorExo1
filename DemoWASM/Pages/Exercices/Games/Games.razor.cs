using DemoWASM.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DemoWASM.Pages.Exercices.Games
{
    public partial class Games
    {
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        public List<Game> Liste { get; set; } = new List<Game>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        public async Task LoadData()
        {
            Liste = await Client.GetFromJsonAsync<List<Game>>("Game");
        }
    }
}
