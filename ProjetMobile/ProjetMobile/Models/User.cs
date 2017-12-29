using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjetMobile.Models
{
    public class User 
    {
        [Key]
        public string Id { get; set; }
        public string Pseudo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserManga> Favoris { get; set; } = new List<UserManga>();
    }
}
