using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YouTube_Stream_API.Controllers
{
    public class AuthController : Controller
    {
        [Route("registration")]
        public ActionResult Registration()
        {
            return View();
        }
        [Route("signin")]
        public ActionResult Signin()
        {
            return View();
        }
    }
}