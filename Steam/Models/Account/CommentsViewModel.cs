using Steam.Data.Entities.Identity;
using Steam.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Steam.Models.News;
using Steam.Models.Game;

namespace Steam.Models.Account
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}
