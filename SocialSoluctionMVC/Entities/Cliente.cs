using SocialSoluctionMVC.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace SocialSoluctionMVC.Entities
{
    public class Cliente: EntityBase, ISoftDelete
    {
        public string Nome { get; set; }
        
        public string Email { get; set; }

        public string CPFCNPJ { get; set; }

        public ICollection<Anuncio> Anuncios { get; set; }

        public bool Excluido { get; set; }

        public DateTime? Exclusao { get; set; }
    }
}
