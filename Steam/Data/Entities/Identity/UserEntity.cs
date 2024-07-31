using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities.Identity
{
    [Table("tblUser")] 
    public class UserEntity : IdentityUser<int>
    {  
        [StringLength(1000)]
        public string Photo { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
        public virtual ICollection<UserGameEntity> UserGames { get; set; } = new List<UserGameEntity>();
        public virtual ICollection<NewsEntity> News { get; set; } = new List<NewsEntity>();
        public virtual ICollection<GameEntity> Games { get; set; } = new List<GameEntity>();


    }
}
