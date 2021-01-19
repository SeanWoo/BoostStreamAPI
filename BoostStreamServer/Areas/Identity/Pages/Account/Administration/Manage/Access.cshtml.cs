using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BoostStreamServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BoostStreamServer.Areas.Identity.Pages.Account.Administration.Manage
{
    public class AccessModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AccessModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Access { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Input = new InputModel();

            var token = _context.Tokens.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (token is not null)
            {
                Input.Access = token.Access;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                var token = _context.Tokens.FirstOrDefault(x => x.Id == id);
                token.Access = Input.Access;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(Url.Content("Users"));
        }
    }
}
