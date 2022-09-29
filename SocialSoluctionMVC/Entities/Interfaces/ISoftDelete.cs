using System;

namespace SocialSoluctionMVC.Entities.Interfaces
{
    public interface ISoftDelete
    {
        public bool Excluido { get; set; }
        public DateTime? Exclusao { get; set; }
    }
}
