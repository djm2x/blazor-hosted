
using System.Net.Http;

namespace MyBlazor.Client.Services
{
    public class UowService
    {
        // public HttpClient Http;
        public UserService users;

        public UowService(HttpClient Http)
        {
            // this.Http = Http;
            users = new UserService(Http);
        }
    }
}