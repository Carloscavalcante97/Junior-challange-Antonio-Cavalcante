using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Junior_Challenge.Domain;
using Junior_Challenge.Communication.Request;

namespace Junior_Challenge.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Junior_Challenge.Domain.AnelContext _context;

        public IndexModel(Junior_Challenge.Domain.AnelContext context)
        {
            _context = context;
        }

        public List<Anel> Aneis { get; set; } = new List<Anel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Aneis = await _context.Aneis.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anel = await _context.Aneis.FindAsync(id);
            if (anel != null)
            {
                _context.Aneis.Remove(anel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
