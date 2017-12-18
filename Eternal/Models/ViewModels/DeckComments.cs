using System.Collections.Generic;

namespace Eternal.Models.ViewModels
{
    public class DeckComments
    {
        public IEnumerable<DeckComment> Comments { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
