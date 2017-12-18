using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eternal.Data;
using Eternal.Models;
using Eternal.Models.ViewModels;

namespace Eternal.ViewComponents
{
    public class DeckCommentsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var comments = new DeckComments
            {
                Comments = await DbHelper.GetDeckComments(id),
                Users = await DbHelper.GetUsers()
            };

            foreach (var comment in comments.Comments)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
                    comment.UserRating = await DbHelper.GetUserDeckCommentRating(comment.DeckCommentID, userId);
                }
                comment.Rating = await DbHelper.GetDeckCommentRating(comment.DeckCommentID);
            }
            comments.Comments = comments.Comments.OrderByDescending(c => c.Rating);

            return View(comments);
        }
    }
}
