using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities.Identity
{
    [Table("tblUser")] 
    public class UserEntity : IdentityUser<int>
    {
        [StringLength(500)]
        public string Photo { get; set; } = null!;
        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
        public ICollection<UserGameEntity> UserGames { get; set; } = new List<UserGameEntity>();
        public ICollection<NewsEntity> News { get; set; } = new List<NewsEntity>();
        public ICollection<DeveloperGameEntity> DevelopedGames { get; set; } = new List<DeveloperGameEntity>();
    }
}
