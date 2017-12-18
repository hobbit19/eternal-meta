using System;
using System.ComponentModel.DataAnnotations;

namespace Eternal.Models
{
    public class Deck
    {
        public int DeckID { get; set; }
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Factions { get; set; }
        [Required]
        public string Guide { get; set; }
        public string DeckList { get; set; }
        public int Rating { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
