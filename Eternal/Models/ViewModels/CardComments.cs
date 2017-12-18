using System.Collections.Generic;

namespace Eternal.Models.ViewModels
{
    public class CardComments
    {
        public IEnumerable<CardComment> Comments { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
