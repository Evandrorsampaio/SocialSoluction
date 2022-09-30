using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using SocialSoluctionMVC.Entities.Enums;
using SocialSoluctionMVC.Entities.Interfaces;

namespace SocialSoluctionMVC.Entities
{
    public class Anuncio: EntityBase, ISoftDelete
    {
        [Display(Name = "Tipo de Anúncio")]
        [Required(ErrorMessage = "Informe o tipo do anúncio.")]
        public int Tipo { set; get; }

        public Cliente Cliente { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Informe o dono do imóvel do anúncio.")]
        [ScaffoldColumn(false)]
        public int ClienteId { get; set; }
        
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Informe o valor do imóvel.")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name = "Publicação")]
        [Required(ErrorMessage = "Informe a data de publicação do anúncio.")]
        [DataType(DataType.Date)]
        public DateTime Publicacao { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição do anúncio.")]
        public string Descricao { get; set; }
        
        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }
        
        public string Localidade { get; set; }
        
        public string UF { get; set; }

        public bool Excluido { get; set; }

        public DateTime? Exclusao { get; set; }
    }
}
