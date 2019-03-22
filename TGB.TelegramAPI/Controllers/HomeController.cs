using Microsoft.AspNetCore.Mvc;

namespace TGB.TelegramAPI.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello, world!";
        }
    }
}
