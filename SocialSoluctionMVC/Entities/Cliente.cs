using System.Collections.Generic;

namespace SocialSoluctionMVC.Entities
{
    public class Cliente: EntityBase
    {
        public string Nome { get; set; }
        
        public string Email { get; set; }

        public string CPFCNPJ { get; set; }

        public ICollection<Anuncio> Anuncios { get; set; }
    }
}
