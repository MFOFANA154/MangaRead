using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetMobile.Models
{
    public class UserManga
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MangaIdM { get; set; }
        public User User { get; set; }
        public Manga Manga { get; set; }
    }
}
