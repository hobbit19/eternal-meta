using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eternal.Models;
using Eternal.Models.ViewModels;
using Eternal.Data;

namespace Eternal.ViewComponents
{
    public class CardCommentsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var comments = new CardComments
            {
                Comments = await DbHelper.GetCardComments(id),
                Users = await DbHelper.GetUsers()
            };

            foreach (var comment in comments.Comments)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(HttpContext.User.Claims.ElementAt(0).Value);
                    comment.UserRating = await DbHelper.GetUserCardCommentRating(comment.CardCommentID, userId);
                }
                comment.Rating = await DbHelper.GetCardCommentRating(comment.CardCommentID);
            }
            comments.Comments = comments.Comments.OrderByDescending(c => c.Rating);

            return View(comments);
        }
    }
}
