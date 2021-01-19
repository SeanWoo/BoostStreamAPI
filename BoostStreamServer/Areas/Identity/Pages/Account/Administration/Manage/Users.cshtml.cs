using BoostStreamServer.Data;
using BoostStreamServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoostStreamServer.Areas.Identity.Pages.Account.Administration.Manage
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> Users => _context.Users.Include(x => x.Token).ToList();

        public async Task<IActionResult> OnPostBanAccountAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user is not null) 
                    user.Banned = !user.Banned;

                await _context.SaveChangesAsync();
            }
            return Page();
        }
    }
}
