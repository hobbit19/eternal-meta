using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eternal.Models.ViewModels
{
    public class FeaturedContent
    {
        public IEnumerable<FeaturedDeck> FeaturedDecks { get; set; }
        public IEnumerable<FeaturedCard> FeaturedCards { get; set; }
    }

    public class FeaturedCard
    {
        public int CardID { get; set; }
        public string ImageUrl { get; set; }
    }

    public class FeaturedDeck
    {
        public int DeckID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Factions { get; set; }
        public string Username { get; set; }
    }

}
