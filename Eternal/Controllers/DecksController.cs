using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eternal.Data;
using Eternal.Models;
using Eternal.Models.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Eternal.Controllers
{
    public class DecksController : Controller
    {


        // Decks/Index

        public async Task<IActionResult> Index(string searchFilter, string currentSearchFilter, string factionFilter, string currentFactionFilter, string userFilter, string currentUserFilter, int? page)
        {
            if (searchFilter != null || factionFilter != null || userFilter != null)
            {
                page = 1;
            }
            else
            {
                searchFilter = currentSearchFilter;
                factionFilter = currentFactionFilter;
                userFilter = currentUserFilter;
            }

            ViewData["SearchFilter"] = searchFilter;
            ViewData["FactionFilter"] = factionFilter;
            ViewData["UserFilter"] = userFilter;

            var decks = await DbHelper.GetDecks();

            if (!String.IsNullOrEmpty(factionFilter))
            {
                var factions = JsonConvert.DeserializeObject<List<string>>(factionFilter);
                if (factions.Count() == 1)
                {
                    decks = decks.Where(d => d.Factions.Contains(factions.First()));
                }
                else
                {
                    decks = decks.Where(d => d.Factions == factionFilter);
                }
            }

            if (!String.IsNullOrEmpty(searchFilter))
            {
                decks = decks.Where(d => d.Name.ToLower().Contains(searchFilter.ToLower()));
            }

            if (!String.IsNullOrEmpty(userFilter))
            {
                var user = await DbHelper.GetUserByUsername(userFilter);
                if (user != null)
                {
                    decks = decks.Where(d => d.UserID == user.UserID);
                }
                else
                {
                    decks = new List<Deck>();
                }
            }

            foreach (var deck in decks)
            {
                deck.Rating = await DbHelper.GetDeckRating(deck.DeckID);
                deck.User = await DbHelper.GetUser(deck.UserID);
            }

            int pageSize = 25;

            return View(PaginatedList<Deck>.CreateAsync(decks.AsQueryable<Deck>(), page ?? 1, pageSize));
        }


        // Decks/Details/{id}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deckDetails = new DeckDetails
            {
                Deck = await DbHelper.GetDeck((int)id),
                Likes = await DbHelper.GetDeckRating((int)id),
                Cards = new List<Card>()
            };
            deckDetails.User = await DbHelper.GetUser(deckDetails.Deck.UserID);
            
            List<Card> deckCards = JsonConvert.DeserializeObject<List<Card>>(deckDetails.Deck.DeckList);

            foreach (var deckCard in deckCards)
            {
                var card = await DbHelper.GetCard(deckCard.CardID);
                card.Count = deckCard.Count;
          
                deckDetails.Cards.Add(card);
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
                deckDetails.UserRating = await DbHelper.GetUserDeckRating((int)id, userId);
            }

            return View(deckDetails);
        }


        // Decks/Edit/{id}

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deck = await DbHelper.GetDeck((int)id);
            if (int.Parse(HttpContext.User.Claims.ElementAt(0).Value) == deck.UserID)
            {
                return View(deck);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Name, Factions, Guide, DeckList")]Deck deck)
        {
            // var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);

            deck.DeckID = id;
            await DbHelper.EditDeck(deck);

            return RedirectToAction("Details", new { id = id });
        }


        // Decks/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name, Factions, Guide, DeckList")]Deck deck)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
            deck.UserID = userId;
            deck.Date = DateTime.Now.Date;
            var deckId = await DbHelper.AddDeck(deck);

            return RedirectToAction("Details", new { id = deckId });
        }


        // Decks/Delete/{id}

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deck = await DbHelper.GetDeck(id);
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
            if (userId == deck.UserID)
            {
                await DbHelper.RemoveDeck(id);

                return new EmptyResult();
            }
            else
            {
                return Forbid();
            }
        }


        // Decks/LoadCollection

        public IActionResult LoadCollection(string searchFilter, string currentSearchFilter, string factionFilter, string currentFactionFilter, string costFilter, string currentCostFilter, int? page)
        {
            return ViewComponent("DeckBuilderCollection",
                new
                {
                    searchFilter = searchFilter,
                    currentSearchFilter = currentSearchFilter,
                    factionFilter = factionFilter,
                    currentFactionFilter = currentFactionFilter,
                    costFilter = costFilter,
                    currentCostFilter = currentCostFilter,
                    page = page
                });
        }


        // Decks/RateDeck

        [HttpPost]
        [Authorize]
        public async Task<int> RateDeck(int id)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
            var userRating = await DbHelper.GetUserDeckRating(id, userId);

            if (userRating == 0)
            {
                await DbHelper.AddDeckRating(id, userId);
                return 1;
            }
            else
            {
                await DbHelper.RemoveUserDeckRating(id, userId);
                return 0;
            }
        }


        // Decks/PostComment

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment(int id, string comment)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);

            var deckComment = new DeckComment
            {
                DeckID = id,
                UserID = userId,
                Comment = comment
            };

            await DbHelper.AddDeckComment(deckComment);

            return new EmptyResult();
        }


        // Decks/LoadComments

        public IActionResult LoadComments(int id)
        {
            return ViewComponent("DeckComments", new { id = id });
        }


        // Decks/EditComment

        [HttpPost]
        [Authorize]
        public async void EditComment(int commentId, string comment)
        {
            await DbHelper.EditDeckComment(commentId, comment);
        }


        // Decks/DeleteComment

        [HttpPost]
        [Authorize]
        public async void DeleteComment(int commentId)
        {
            await DbHelper.DeleteDeckComment(commentId);
        }


        // Decks/ReportComment

        [HttpPost]
        [Authorize]
        public async void ReportComment(int commentId)
        {
            await DbHelper.ReportDeckComment(commentId);
        }


        // Decks/RateComment

        [HttpPost]
        [Authorize]
        public async Task<int> RateComment(int commentId)
        {
            var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
            var userRating = await DbHelper.GetUserDeckCommentRating(commentId, userId);

            if (userRating == 0)
            {
                await DbHelper.AddDeckCommentRating(commentId, userId);
                return 1;
            }
            else
            {
                await DbHelper.RemoveDeckCommentRating(commentId, userId);
                return 0;
            }
        }

    }
}