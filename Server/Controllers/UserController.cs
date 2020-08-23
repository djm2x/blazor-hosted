using Microsoft.AspNetCore.Mvc;
using MyBlazor.Server.Models;
using MyBlazor.Shared;
namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : SuperController<User>
    {
        public UserController(AppDbContext context) : base(context)
        { }
        
    }
}
