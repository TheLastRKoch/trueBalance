using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRueBalance.Controllers
{
    public class UserSettingController : Controller
    {
        [Authorize(Roles = "Developer,Administrator")]
        public IActionResult Index ()
        {
            return View();
        }
    }
}
