namespace CinemaHour.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using CinemaHour.Data.Models;
    using CinemaHour.Services.Mapping;
    using CinemaHour.Web.ViewModels.Movies.Edit;

    public class MovieEditViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Movie name")]
        public string Name { get; set; }

        // Length of the movie in minutes
        [Required]
        [Range(30, 300)]
        public int Length { get; set; }

        // Release Date of the Movie
        [Display(Name = "Premiere Date")]
        public DateTime ReleaseDate { get; set; }

        // Movie Poster with default picture
        [Display(Name = "Movie Poster")]
        public string PosterUrl { get; set; }

        // Movie Language dub
        public string Language { get; set; }

        // Average Movie Rating
        [Range(0, 5)]
        public float? Rating { get; set; }

        [Display(Name = "IMDB Link")]
        public string IMDBLink { get; set; }

        [Display(Name = "Trailer Link")]
        public string TrailerLink { get; set; }

        public string Summary { get; set; }

        public virtual ICollection<MovieActorsEditInputModel> Actors { get; set; }

        public virtual ICollection<MovieGenresEditInputModel> Genres { get; set; }

        public virtual ICollection<MovieDirectorEditInputModel> Directors { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            // Actors mapping
            configuration.CreateMap<MovieActors, MovieActorsEditInputModel>()
                .ForMember(
                    x => x.FirstName,
                    opt => opt.MapFrom(x => x.Actor.FirstName))
                .ForMember(
                    x => x.LastName,
                    opt => opt.MapFrom(x => x.Actor.LastName))
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(x => x.Actor.Id));

            // Genres mapping
            configuration.CreateMap<MovieGenre, MovieGenresEditInputModel>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(x => x.GenreId))
                .ForMember(
                    x => x.Name,
                    opt => opt.MapFrom(x => x.Genre.Name));

            // Directors mapping
            configuration.CreateMap<MovieDirector, MovieDirectorEditInputModel>()
                .ForMember(
                    x => x.Id,
                    opt => opt.MapFrom(d => d.DirectorId))
                .ForMember(
                    x => x.FullName,
                    opt => opt.MapFrom(d => d.Director.FullName));
        }
    }
}
