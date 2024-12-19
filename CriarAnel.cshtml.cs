using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Junior_Challenge.Domain;
using Junior_Challenge.Communication.Request;
using System.Threading.Tasks;

namespace Junior_challange_.Pages
{
    public class CriarAnelModel : PageModel
    {
        private AnelContext _context;

        [BindProperty]
        public Anel Anel { get; set; }
        public CriarAnelModel(AnelContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var anel = new Anel();
            if (await TryUpdateModelAsync<Anel>(
                anel,
                "anel",
                a => a.Nome, a => a.Poder, a => a.Portador, a => a.Forjador, a => a.Imagem))
            {
                _context.Aneis.Add(anel);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}