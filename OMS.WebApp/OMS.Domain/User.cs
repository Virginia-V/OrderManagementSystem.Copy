using Microsoft.AspNetCore.Identity;
using OMS.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Domain
{
    public class User : IdentityUser<int>, IEntity, IProfile
    {
        public string? Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactInfo { get; set; }
        public bool IsSocialAccount { get; set; }
        [NotMapped]
        public string Name => string.Join(" ", FirstName, LastName);

        public override bool Equals(object? obj)
        {
            if (obj.GetType() != GetType()) return false;
            var entity = obj as Entity;
            return entity?.Id == Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
