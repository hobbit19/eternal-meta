namespace Eternal.Models
{
    public class Card
    {
        public int CardID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Factions { get; set; }
        public string Set { get; set; }
        public string Rarity { get; set; }
        public string Cost { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }
    }
}
