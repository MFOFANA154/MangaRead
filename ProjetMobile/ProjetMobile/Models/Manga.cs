using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xamarin.Forms;

namespace ProjetMobile.Models
{
    
    public class Manga 
    {
        [Key]
        public string IdM { get; set; }
        public string Titre { get; set; }
        [NotMapped]
        public ImageSource Image { get; set; }
        public string Descriptif { get; set; }
        public string Status { get; set; }
        public string Auteur { get; set; }
        public string Genre { get; set; }
        public int Episode { get; set; }

        public ICollection<UserManga> UserList { get; set; } = new List<UserManga>();
    }
}
