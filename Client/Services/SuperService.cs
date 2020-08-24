using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MyBlazor.Client.Services
{
    public class SuperService<T>
    {
        // [Inject]
        public HttpClient Http; // { get; set; }
        public string controller = "";

        public SuperService(string controller, HttpClient Http)
        {
            this.controller = controller;
            this.Http = Http;
        }

        public Task<Res<T>> GetList(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            return Http.GetFromJsonAsync<Res<T>>($"api/{controller}/getAll/{startIndex}/{pageSize}/{sortBy}/{sortDir}");
        }



        public Task<T[]> Get()
        {
            return Http.GetFromJsonAsync<T[]>($"api/{controller}/get");
        }

        public Task<T> GetById(int id)
        {
            return Http.GetFromJsonAsync<T>($"api/{controller}/get/{id}");
        }

        public Task<HttpResponseMessage> Post(T model)
        {
            return Http.PostAsJsonAsync<T>($"api/{controller}/post", model);
        }

        public Task<HttpResponseMessage> Put(int id, T model)
        {
            return Http.PutAsJsonAsync<T>($"api/{controller}/put/{id}", model);
        }

        public Task<HttpResponseMessage> Delete(int id)
        {
            return Http.DeleteAsync($"api/{controller}/delete/{id}");
        }
    }

    public class Res<T> {
        public T[] list {set; get;}

        public int count { get; set; }
    }
}