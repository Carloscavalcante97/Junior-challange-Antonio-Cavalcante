using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Junior_Challenge.Communication.Request;
using Junior_Challenge.Domain;

namespace Junior_challange_.Pages
{
    public class DetalharAnelModel : PageModel
    {
        private readonly Junior_Challenge.Domain.AnelContext _context;

        public DetalharAnelModel(Junior_Challenge.Domain.AnelContext context)
        {
            _context = context;
        }

        public Anel Anel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anel = await _context.Aneis.FirstOrDefaultAsync(m => m.Id == id);
            if (anel == null)
            {
                return NotFound();
            }
            else
            {
                Anel = anel;
            }
            return Page();
        }
    }
}
