namespace CinemaHour.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int MovieId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
