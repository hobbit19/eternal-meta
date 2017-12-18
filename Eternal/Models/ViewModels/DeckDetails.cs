using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eternal.Models.ViewModels
{
    public class DeckDetails
    {
        public User User { get; set; }
        public Deck Deck { get; set; }
        public List<Card> Cards { get; set; }
        public int DeckCraftCost { get; set; }
        public int Likes { get; set; }
        public int UserRating { get; set; }
    }
}
