using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Junior_Challenge.Communication.Request;
using Junior_Challenge.Domain;

namespace Junior_challange_.Pages
{
    public class EditarAnelModel : PageModel
    {
        private readonly Junior_Challenge.Domain.AnelContext _context;

        public EditarAnelModel(Junior_Challenge.Domain.AnelContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Anel Anel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anel =  await _context.Aneis.FirstOrDefaultAsync(m => m.Id == id);
            if (anel == null)
            {
                return NotFound();
            }
            Anel = anel;
            return Page();
        }

      
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Anel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnelExists(Anel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AnelExists(Guid id)
        {
            return _context.Aneis.Any(e => e.Id == id);
        }
    }
}
