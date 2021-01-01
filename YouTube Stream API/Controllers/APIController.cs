using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using YouTube_Stream_API.Models;
using Files = System.IO.File;

namespace YouTube_Stream_API.Controllers
{
    [ApiController]
    [Route("api")]
    public class APIController : ControllerBase
    {
        private readonly ILogger<APIController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private ApplicationContext db;

        public APIController(IWebHostEnvironment appEnvironment, ILogger<APIController> logger, ApplicationContext context)
        {
            _appEnvironment = appEnvironment;
            _logger = logger;
            db = context;
        }
        [Route("checkProxy")]
        public string CheckProxy()
        {
            return Request.Headers["X-Forwarded-For"];
        }
        [Route("telemetry"), HttpPost]
        public void Telemetry([FromForm]TelemetryMessage telemetry)
        {
            if (!string.IsNullOrWhiteSpace(telemetry.Login) && !string.IsNullOrWhiteSpace(telemetry.Message))
            {
                var userFromDB = db.Users.FirstOrDefault(x => x.Name == telemetry.Login);
                if (userFromDB == null) return;
                var t = new Telemetry(DateTime.Now, Request.Headers["X-Forwarded-For"], telemetry.Login, telemetry.Message);
                db.Telemetrys.Add(t);
                db.SaveChanges();
            }
        }
        [Route("getAccess"), HttpGet]
        public string GetAccess(string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password))
            {
                var userFromDB = db.Users.FirstOrDefault(x => x.Name == login && x.Password == password);
                if (userFromDB == null) return "error";
                return userFromDB.Access;
            }
            return "error";
        }
        [Route("getVersion"), HttpGet]
        public string GetVersion()
        {
            string file_path;
            FileVersionInfo fileInfo;
            StringBuilder builder = new StringBuilder();

            file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/Main/BoostStream.exe");
            fileInfo = FileVersionInfo.GetVersionInfo(file_path);
            builder.Append("Main:" + fileInfo.FileVersion + "\n");

            foreach (var file in Directory.GetFiles("Files/Main/Modules"))
            {
                fileInfo = FileVersionInfo.GetVersionInfo(file);
                builder.Append(fileInfo.ProductName + ":" + fileInfo.FileVersion + "\n");
            }

            return builder.ToString();
        }
        [Route("patchnote"), HttpGet]
        public string GetPatchNote()
        {
            return Files.ReadAllText("Files/patchnote.txt");
        }
        [Route("update"), HttpGet]
        public ActionResult Update(string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password))
            {
                var userFromDB = db.Users.FirstOrDefault(x => x.Name == login && x.Password == password);
                if (userFromDB == null) return RedirectToAction("error");
                var access = userFromDB.Access.ToUpper().Split(',');

                var sourceDirectory = Path.Combine(_appEnvironment.ContentRootPath, "Files/Main");
                var destinationPath = Path.Combine(_appEnvironment.ContentRootPath, "Files/Archive.zip");

                if (Files.Exists(destinationPath)) Files.Delete(destinationPath);

                ZipFile.CreateFromDirectory(sourceDirectory, destinationPath);
                using (ZipArchive archive = ZipFile.Open(destinationPath, ZipArchiveMode.Update))
                {
                    FileVersionInfo fileInfo;
                    if (!access.Contains("ALL"))
                    {
                        foreach (var file in Directory.GetFiles("Files/Main/Modules"))
                        {
                            fileInfo = FileVersionInfo.GetVersionInfo(file);
                            if(!access.Contains(fileInfo.ProductName.ToUpper())) archive.GetEntry("Modules/" + fileInfo.ProductName + ".dll").Delete();
                        }
                    }
                }
                return PhysicalFile(destinationPath, "application/zip", "update.zip");
            }
            return RedirectToAction("error");
        }
        [Route("signin"), HttpPost]
        public bool signin([FromForm]AuthUser user)
        {
            if (!string.IsNullOrWhiteSpace(user.Login) && !string.IsNullOrWhiteSpace(user.Password))
            {
                var userFromDB = db.Users.FirstOrDefault(x => x.Name == user.Login && x.Password == user.Password);
                if (userFromDB != null) return true;
            }
            return false;
        }
        [Route("addNewUser"), HttpPost]
        public bool AddNewUser([FromForm]AuthUser user)
        {
            if (!string.IsNullOrWhiteSpace(user.Token) && !string.IsNullOrWhiteSpace(user.Login) && !string.IsNullOrWhiteSpace(user.Password))
            {
                Token tkn = db.Tokens.FirstOrDefault(x => x.Chars == user.Token && x.Active == true);
                if (tkn == null) return false;

                tkn.Active = false;
                db.Tokens.Update(tkn);

                var hashPassword = GetHash(user.Password);

                db.Users.Add(new User(user.Login, hashPassword, tkn.Access));
                db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        [Route("error"), HttpGet]
        public string Error()
        {
            return "error";
        }

        private string GetHash(string input)
        {
            var result = "";
            using (SHA1Managed manager = new SHA1Managed())
            {
                var bytes = manager.ComputeHash(Encoding.Default.GetBytes(input));
                var sb = new StringBuilder();

                foreach (var item in bytes)
                    sb.Append(item.ToString("x2"));
                result = sb.ToString();
            }
            return result;
        }
    }
}
