using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eternal.Data;
using Eternal.Models;
using Newtonsoft.Json;

namespace Eternal.ViewComponents
{
    public class DeckBuilderCollectionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string searchFilter, string currentSearchFilter, string factionFilter, string currentFactionFilter, string costFilter, string currentCostFilter, int? page)
        {
            if (searchFilter != null || factionFilter != null || costFilter != null)
            {
                page = 1;
            }
            else
            {
                searchFilter = currentSearchFilter;
                factionFilter = currentFactionFilter;
                costFilter = currentCostFilter;
            }

            ViewData["SearchFilter"] = searchFilter;
            ViewData["FactionFilter"] = factionFilter;
            ViewData["CostFilter"] = costFilter;

            var cards = await DbHelper.GetCards();

            if (!String.IsNullOrEmpty(factionFilter))
            {
                var tempCards = new List<Card>();
                var factionList = new List<String> { "Fire", "Time", "Justice", "Primal", "Shadow" };
                var factions = JsonConvert.DeserializeObject<List<string>>(factionFilter);

                if (factions.Count() == 1)
                {
                    if (factions.First() == "Multifaction")
                    {
                        cards = cards.Where(c => c.Factions.Length >= 15);
                    }
                    else
                    {
                        cards = cards.Where(c => c.Factions.Contains(factions.First()) || c.Factions.Contains("Factionless"));
                    }
                }
                else
                {
                    if (factions.Contains("Multifaction"))
                    {
                        foreach (var faction in factions)
                        {
                            if (faction != "Multifaction")
                            {
                                tempCards.AddRange(cards.Where(c => c.Factions.Length >= 15 && c.Factions.Contains(faction) && !tempCards.Contains(c)));
                            }
                        }
                        if (factions.Count() >= 3)
                        {
                            foreach (var faction in factionList)
                            {
                                if (!factions.Contains(faction))
                                {
                                    tempCards.RemoveAll(c => c.Factions.Contains(faction));
                                }
                            }
                        }
                    }
                    else
                    {
                        tempCards = cards.ToList();
                        foreach (var faction in factionList)
                        {
                            if (!factions.Contains(faction))
                            {
                                tempCards.RemoveAll(c => c.Factions.Contains(faction));
                            }
                        }
                        tempCards.AddRange(cards.Where(c => c.Factions == "Factionless"));
                    }
                    cards = tempCards.OrderBy(c => c.CardID);
                }
            }

            if (!String.IsNullOrEmpty(searchFilter))
            {
                cards = cards.Where(c =>
                    c.Name.ToLower().Contains(searchFilter.ToLower()) || c.Type.ToLower().Contains(searchFilter.ToLower()) ||
                    c.Text.ToLower().Contains(searchFilter.ToLower()) || c.Rarity.ToLower() == searchFilter.ToLower());
            }

            if (!String.IsNullOrEmpty(costFilter))
            {
                var cost = int.Parse(costFilter);
                if (cost == 7)
                {
                    cards = cards.Where(c => int.Parse(c.Cost) >= cost);
                }
                else
                {
                    cards = cards.Where(c => int.Parse(c.Cost) == cost);
                }
            }

            int pageSize = 8;

            return View(PaginatedList<Card>.CreateAsync(cards.AsQueryable<Card>(), page ?? 1, pageSize));
        }
    }
}
