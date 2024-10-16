﻿using DemoWASM.Models;
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

        [Parameter]
        public int? id { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await LoadData();
        }

        public async Task LoadData()
        {
            if (id is not null)
            {
                string token = await JS.InvokeAsync<string>("localStorage.getItem", "token");
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                GameForm = await Client.GetFromJsonAsync<GamePost>("Game/" + id);
            }
            else
                GameForm = new GamePost();
        }

        public async Task Add()
        {
            string token = await JS.InvokeAsync<string>("localStorage.getItem", "token");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (id is not null)
            {
                await Client.PutAsJsonAsync("Game/" + id, GameForm);
            }
            else
            {
                await Client.PostAsJsonAsync("Game", GameForm);
            }
            Nav.NavigateTo("Exo3");
        }

        public async Task Return()
        {
            Nav.NavigateTo("Exo3");
        }
    }
}
