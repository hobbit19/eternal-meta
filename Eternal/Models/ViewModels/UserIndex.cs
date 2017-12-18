using System.Collections.Generic;

namespace Eternal.Models.ViewModels
{
    public class UserIndex
    {
        public User User { get; set; }
        public IEnumerable<Deck> Decks { get; set; }
    }
}
