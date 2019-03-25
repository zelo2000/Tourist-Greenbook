using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TGB.TelegramAPI.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello from Tourist Greenbook!";
        }
    }
}
