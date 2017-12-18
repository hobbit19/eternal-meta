using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eternal.Data;
using Eternal.Models;
using Eternal.Models.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Eternal.Controllers
{
    public class CardsController : Controller
    {
        public async Task<IActionResult> Index(string searchFilter, string currentSearchFilter, string factionFilter, string currentFactionFilter, string costFilter, string currentCostFilter, int? page)
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
                List<Card> tempCards = new List<Card>();
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

            int pageSize = 24;

            return View(PaginatedList<Card>.CreateAsync(cards.AsQueryable<Card>(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardDetails = new CardDetails
            {
                Card = await DbHelper.GetCard((int)id),
                Likes = await DbHelper.GetCardLikes((int)id)
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
                cardDetails.UserRating = await DbHelper.GetUserCardRating((int)id, userId);
            }

            return View(cardDetails);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<int> RateCard(int id)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
            var userRating = await DbHelper.GetUserCardRating(id, userId);

            if (userRating == 0)
            {
                await DbHelper.AddCardRating(id, userId);
                return 1;
            }
            else
            {
                await DbHelper.RemoveCardRating(id, userId);
                return 0;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment(int id, string comment)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);

            var cardComment = new CardComment
            {
                CardID = id,
                UserID = userId,
                Comment = comment,
                Date = DateTime.Now
            };

            await DbHelper.AddCardComment(cardComment);

            return new EmptyResult();
        }

        public IActionResult LoadComments(int id)
        {
            return ViewComponent("CardComments", new { id = id });
        }

        [HttpPost]
        [Authorize]
        public async void EditComment(int commentId, string comment)
        {
            await DbHelper.EditCardComment(commentId, comment);
        }

        [HttpPost]
        [Authorize]
        public async void DeleteComment(int commentId)
        {
            await DbHelper.DeleteCardComment(commentId);
        }

        [HttpPost]
        [Authorize]
        public async void ReportComment(int commentId)
        {
            await DbHelper.ReportCardComment(commentId);
        }

        [HttpPost]
        [Authorize]
        public async Task<int> RateComment(int commentId)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
            var userRating = await DbHelper.GetUserCardCommentRating(commentId, userId);

            if (userRating == 0)
            {
                await DbHelper.AddCardCommentRating(commentId, userId);
                return 1;
            }
            else
            {
                await DbHelper.RemoveCardCommentRating(commentId, userId);
                return 0;
            }
        }
    }
}