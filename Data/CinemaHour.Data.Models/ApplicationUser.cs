// ReSharper disable VirtualMemberCallInConstructor
namespace CinemaHour.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CinemaHour.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Favourites = new HashSet<UserFavourite>();
            this.Watched = new HashSet<UserWatched>();
        }

        //public byte[] Avatar { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int TotalTimeWatched => this.Watched.Sum(m => m.Movie.Length);

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<UserFavourite> Favourites { get; set; }

        public virtual ICollection<UserWatched> Watched { get; set; }
    }
}
