using SocialSoluctionMVC.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialSoluctionMVC.Entities
{
    public class Cliente: EntityBase, ISoftDelete
    {
        [Required(ErrorMessage = "Informe o Nome do Cliente")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Email do Cliente")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Informe o endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o CPF ou CNPJ do Cliente")]
        [Display(Name = "CPF ou CNPJ")]
        [MaxLength(17)]
        [RegularExpression("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})"
            , ErrorMessage = "Informe um CPF ou CPNJ válido.")]
        public string CPFCNPJ { get; set; }

        public ICollection<Anuncio> Anuncios { get; set; }

        public bool Excluido { get; set; }

        public DateTime? Exclusao { get; set; }
    }
}
