using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjetMobile.Models;
using ProjetMobile.Services;
using Microsoft.EntityFrameworkCore;

namespace ProjetMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        public User User { get; set; }
        //SignupPage GetSignup;
        //public string userEmailHP { get; set; }
        public HomePage ()
		{
            
            InitializeComponent();
            User = new User { };
            //userEmailHP = "";
            BindingContext = this;
           /* if (!(GetSignup is null))
            {
                userEmailHP = GetSignup.userEmail;
            }*/

        }
      
        async void OnLogin(object sender, EventArgs e)
        {
            
            
            var userCourant = this.User;

            User userBase;

            using (MyDbContext context = new MyDbContext())
            {
                userBase = await context.Users.Where(u =>
             u.Email.Equals(userCourant.Email) && u.Password.Equals(userCourant.Password)).FirstOrDefaultAsync();

               
            }           
            

            if (userBase !=null)
            {
                var accueil = new Accueil();
                accueil.BindingContext = this;
                await Navigation.PushAsync(accueil);
            }
            else
            {
                await DisplayAlert("Oups", "Identifiants ou mot de passe incorrect. \nMerci de vous inscrire ou bien de verifier à nouveau vos identifiants.", "OK");
               

            }
        }
        async void OnSignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }

      

    }
}
