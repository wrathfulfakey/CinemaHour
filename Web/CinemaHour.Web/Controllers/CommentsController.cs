namespace CinemaHour.Web.Controllers
{
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var movieId = await this.commentsService.Delete(id);

            return this.RedirectToAction("Details", "Movies", new { id = movieId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            var parentId =
                input.ParentId == 0 ?
                    (int?)null :
                    input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInMovieId(parentId.Value, input.MovieId))
                {
                    return this.BadRequest();
                }
            }

            if (input.Content == null)
            {
                return this.NoContent();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(input.MovieId, userId, input.Content, parentId);
            return this.RedirectToAction("Details", "Movies", new { id = input.MovieId });
        }
    }
}
