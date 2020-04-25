namespace CinemaHour.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Common.Repositories;
    using CinemaHour.Data.Models;
    using CinemaHour.Data.Models.Enum;
    using CinemaHour.Services.Data.Interfaces;
    using CinemaHour.Services.Data.ViewModels.Actors;
    using CinemaHour.Services.Mapping;

    public class ActorsService : IActorsService
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;
        private readonly IRepository<MovieActors> movieActorsRepository;

        public ActorsService(
            IDeletableEntityRepository<Actor> actorsRepository,
            IRepository<MovieActors> movieActorsRepository)
        {
            this.actorsRepository = actorsRepository;
            this.movieActorsRepository = movieActorsRepository;
        }

        public ICollection<T> GetAll<T>(int? count = null)
        {
            IQueryable<Actor> query = this.actorsRepository.All()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        // Non-generic Get All Method
        public bool CheckifExists(string firstName, string lastName)
        {
            var actor = this.actorsRepository.All()
                .Where(x => x.FirstName == firstName && x.LastName == lastName)
                .FirstOrDefault();

            if (actor == null)
            {
                return false;
            }

            return true;
        }

        // Admins can [create], [delete] AND [edit] (same with directors, movies, comments, users)
        // new actors (movies can be added AFTER we add actors and directors)
        public async Task<string> CreateActorAsync(CreateActorViewModel input)
        {
            var genderAsEnum = Enum.Parse<Gender>(input.Gender);

            var actor = new Actor
            {
                ImageUrl = input.Image,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Info = input.Info,
                Gender = genderAsEnum,
                BirthDate = input.BirthDate,
            };

            if (this.actorsRepository.All().Any(x =>
                x.FirstName == input.FirstName &&
                x.LastName == input.LastName &&
                x.BirthDate == input.BirthDate))
            {
                return "There is already an actor with the same name.";
            }

            await this.actorsRepository.AddAsync(actor);
            await this.actorsRepository.SaveChangesAsync();

            return actor.Id;
        }

        public T GetById<T>(string id)
        {
            var actor = this.actorsRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return actor;
        }

        public async Task EditActorAsync(EditActorInputModel input)
        {
            var actor = await this.actorsRepository.GetByIdWithDeletedAsync(input.Id);

            actor.ImageUrl = input.ImageUrl;
            actor.Info = input.Info;
            actor.FirstName = input.FirstName;
            actor.LastName = input.LastName;
            var genderAsEnum = Enum.Parse<Gender>(input.Gender);
            actor.Gender = genderAsEnum;
            actor.BirthDate = input.BirthDate;

            this.actorsRepository.Update(actor);
            await this.actorsRepository.SaveChangesAsync();
        }

        public async Task DeleteActorAsync(string id)
        {
            var actor = await this.actorsRepository.GetByIdWithDeletedAsync(id);

            this.actorsRepository.Delete(actor);
            await this.actorsRepository.SaveChangesAsync();
        }

        public async Task HardDeleteActorAsync(string id)
        {
            var actor = await this.actorsRepository.GetByIdWithDeletedAsync(id);

            var moviesToDeleteActor = this.movieActorsRepository.All()
                .Where(x => x.ActorId == actor.Id);
            foreach (var movie in moviesToDeleteActor)
            {
                this.movieActorsRepository.Delete(movie);
            }

            this.actorsRepository.HardDelete(actor);
            await this.actorsRepository.SaveChangesAsync();
        }
    }
}
