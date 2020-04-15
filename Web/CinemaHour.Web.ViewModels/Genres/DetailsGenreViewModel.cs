namespace CinemaHour.Web.ViewModels.Genres
{
    using System.Collections.Generic;

    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;

    public class DetailsGenreViewModel : IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<MovieDetailsGenreViewModel> Movies { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage
        {
            get
            {
                if (this.CurrentPage >= this.PagesCount)
                {
                    return 1;
                }

                return this.CurrentPage + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (this.CurrentPage <= 1)
                {
                    return this.PagesCount;
                }

                return this.CurrentPage - 1;
            }
        }
    }
}
