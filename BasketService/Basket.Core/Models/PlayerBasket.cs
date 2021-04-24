using System.Collections.Generic;

namespace Basket.Core.Models
{
    public class PlayerBasket
    {
        public PlayerBasket()
        {
            Tournament = new List<Tournament>();
        }

        public List<Tournament> Tournament { get; set; }

        public void AddItem(Tournament tournament)
        {
            Tournament.Add(tournament);
        }
    }
}
