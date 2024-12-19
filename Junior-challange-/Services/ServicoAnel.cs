using Junior_Challenge.Communication.Request;
using Junior_Challenge.Domain;

namespace Junior_challange_.Services
{
    public class ServicosAnel
    {
        private readonly AnelContext _context;
        private readonly Dictionary<string, int> _limite = new()
        {
            { "Elfo", 3 },
            { "Anão", 7 },
            { "Homem", 9 },
            { "Sauron", 1 }
        };

        public ServicosAnel(AnelContext context)
        {
            _context = context;
        }

        public List<Anel> ListarTodosAneis()
        {
            return _context.Aneis.ToList();
        }

        public Anel GetRingById(Guid id)
        {
            var anel = _context.Aneis.Find(id);
            if (anel == null)
            {
                throw new Exception($"Anel com ID {id} não encontrado.");
            }
            return anel;
        }

        private void ValidandoPortador(string portador)
        {
            if (!_limite.ContainsKey(portador))
            {
                throw new Exception("Portador inválido");
            }

            int contador = _context.Aneis.Count(r => r.Portador == portador);
            if (contador >= _limite[portador])
            {
                throw new Exception("Limite de anéis atingido");
            }
        }

        public void CriarAnel(Anel anel)
        {
            ValidandoPortador(anel.Portador);
            _context.Aneis.Add(anel);
            _context.SaveChanges();
        }

        public bool AtualizarAnel(Anel anelAtualizado)
        {
            var anel = _context.Aneis.FirstOrDefault(r => r.Id == anelAtualizado.Id);
            if (anel == null)
            {
                throw new Exception("Anel não encontrado");
            }
            ValidandoPortador(anelAtualizado.Portador);
            anel.Nome = anelAtualizado.Nome;
            anel.Portador = anelAtualizado.Portador;
            anel.Forjador = anelAtualizado.Forjador;
            anel.Poder = anelAtualizado.Poder;
            anel.Imagem = anelAtualizado.Imagem;

            _context.SaveChanges();
            return true;
        }

        public bool deletarAnel(Guid id)
        {
            var anel = _context.Aneis.FirstOrDefault(r => r.Id == id);
            if (anel == null)
            {
                throw new Exception("Anel não encontrado");
            }
            _context.Aneis.Remove(anel);
            _context.SaveChanges();
            return true;
        }
    }
}