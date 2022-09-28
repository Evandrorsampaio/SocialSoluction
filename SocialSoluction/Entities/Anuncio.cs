﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using SocialSoluction.Entities.Enums;

namespace SocialSoluction.Entities
{
    public class Anuncio: EntityBase
    {
        public int Tipo { set; get; }

        public Cliente Cliente { get; set; }

        public int ClienteId { get; set; }
        
        public decimal Valor { get; set; }
        
        public DateTime Publicacao { get; set; }
        
        public string Descricao { get; set; }
        
        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }
        
        public string Localidade { get; set; }
        
        public string UF { get; set; }

     
    }
}