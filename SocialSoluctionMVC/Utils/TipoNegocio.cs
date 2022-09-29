using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Internal;
using SocialSoluctionMVC.Entities.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SocialSoluctionMVC.Utils
{
    public class TipoAnuncio
    {
        private const int VendaId = (int)TipoNegocio.Venda;
        private const string Venda = "Venda";
        private const int AluguelId = (int)TipoNegocio.Aluguel;
        private const string Aluguel = "Aluguel";
        public int Id { get; set; }

        public string Nome { get; set; }

        public static List<TipoAnuncio> TiposAnuncios()
        {
            var result = new List<TipoAnuncio>();
            result.Add(new TipoAnuncio { Id = 0, Nome = "" });
            result.Add(new TipoAnuncio { Id = VendaId, Nome =  Venda});
            result.Add(new TipoAnuncio { Id = AluguelId, Nome = Aluguel});
            return result;
        }

        public static string TipoNome(int tipo)
        {
            var result = TiposAnuncios()
                .Where(t => t.Id == tipo)
                .FirstOrDefault().Nome;
            
            return result;
        }

    }
}
