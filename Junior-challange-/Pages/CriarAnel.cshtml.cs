using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Junior_Challenge.Domain;
using Junior_Challenge.Communication.Request;

namespace Junior_challange_.Pages
{
    public class CriarAnelModel : PageModel
    {
        private AnelContext _context;

        [BindProperty]
        public Anel Anel { get; set; } = new Anel();
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string errorMessage = string.Empty;
            var totalAnéis = _context.Aneis.Count(a => a.Portador == Anel.Portador);

            switch (Anel.Portador)
            {
                case "Elfo":
                    if (totalAnéis >= 3)
                    {
                        errorMessage = "Não é possível criar mais de 3 anéis para Elfos.";
                    }
                    break;
                case "Anão":
                    if (totalAnéis >= 7)
                    {
                        errorMessage = "Não é possível criar mais de 7 anéis para Anões.";
                    }
                    break;
                case "Homem":
                    if (totalAnéis >= 9)
                    {
                        errorMessage = "Não é possível criar mais de 9 anéis para Homens.";
                    }
                    break;
                case "Sauron":
                    if (totalAnéis >= 1)
                    {
                        errorMessage = "Já existe um anel para Sauron.";
                    }
                    break;
                default:
                    errorMessage = "Tipo de anel inválido.";
                    break;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return Page();
            }
            Anel.Id = Guid.NewGuid();
            _context.Aneis.Add(Anel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
