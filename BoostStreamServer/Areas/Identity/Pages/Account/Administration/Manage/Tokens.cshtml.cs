using BoostStreamServer.Data;
using BoostStreamServer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoostStreamServer.Areas.Identity.Pages.Account.Administration.Manage
{
    public class TokensModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TokensModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<Token> Tokens => _context.Tokens.ToList();

        public class InputModel
        {
            [Display(Name = "Права доступа")]
            public string Access { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var token = new Token
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    Access = Input.Access
                };
                await _context.Tokens.AddAsync(token);

                await _context.SaveChangesAsync();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteTokenAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                var token = _context.Tokens.FirstOrDefault(x => x.Id == id);
                if (token is not null && token.Active)
                {
                    _context.Tokens.Remove(token);
                    await _context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }
}
