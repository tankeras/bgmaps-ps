// ReSharper disable VirtualMemberCallInConstructor
namespace Mapsps.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Mapsps.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Upvotes = new HashSet<Upvote>();
            this.ConfirmedPets = new HashSet<ConfirmedPet>();
        }

        public ICollection<ConfirmedPet> ConfirmedPets { get; set; }

        public ICollection<Upvote> Upvotes { get; set; }

        public string Region { get; set; }


        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
