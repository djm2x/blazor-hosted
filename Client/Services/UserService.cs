using System.Net.Http;
using Microsoft.AspNetCore.Components;
using MyBlazor.Shared;

namespace MyBlazor.Client.Services
{
    public class UserService: SuperService<User>
    {
        public UserService(HttpClient Http) : base("user", Http) { }
    }
}