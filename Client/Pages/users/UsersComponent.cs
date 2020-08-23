using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyBlazor.Client.Services;
using MyBlazor.Shared;

namespace MyBlazor.Client.Pages.users
{
    public class UsersComponent : ComponentBase
    {
        protected User[] users;
        protected bool edit = false;
        protected User newUser = new User();

        [Inject]
        protected UowService uow { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            users = await uow.users.Get();
        }
        protected async Task SubmitUser()
        {
            Console.WriteLine("here we go");
            if (edit == false)
            {
                await Http.PostAsJsonAsync<User>("api/user", newUser);
                await OnInitializedAsync();
                newUser = new User();
            }
            else
            {
                await Http.PutAsJsonAsync<User>("api/user/" + newUser.Id, newUser);
                await OnInitializedAsync();
                edit = false;
                newUser = new User();
            }
        }
        protected async Task DeleteUser(int id)
        {
            await Http.DeleteAsync("api/user/" + id.ToString());
            await OnInitializedAsync();
        }
        protected async Task GetUser(int id)
        {
            newUser = await Http.GetJsonAsync<User>("api/user/" + id.ToString());
            edit = true;

        }
    }
}