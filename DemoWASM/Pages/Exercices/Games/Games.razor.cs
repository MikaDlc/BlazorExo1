using DemoWASM.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace DemoWASM.Pages.Exercices.Games
{
    public partial class Games
    {
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }
        [Inject]
        public NavigationManager Nav { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }

        public List<Game> Liste { get; set; } = new List<Game>();
        public ClaimsPrincipal? User { get; set; }
        private bool Disabled { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            var authState = await authenticationState;
            User = authState?.User;
        }

        public async Task LoadData()
        {
            Liste = await Client.GetFromJsonAsync<List<Game>>("Game");
        }

        public async Task Add()
        {
            Nav.NavigateTo("Exo3/Add");

        }

        public async Task Delete(int Id)
        {
            Disabled = true;
            string token = await JS.InvokeAsync<string>("localStorage.getItem", "token");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage m = await Client.DeleteAsync("Game/" + Id);
            if (m.IsSuccessStatusCode)
            {
                Disabled = false;
                Nav.Refresh();
            }

        }

        public async Task Edit()
        {
            Nav.NavigateTo("Exo3/Add");
            // Interceptor
        }
    }
}
