using Microsoft.EntityFrameworkCore;
using ProjetMobile.Models;
using ProjetMobile.Services;
using ProjetMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjetMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignUpViewModel viewModel;
        public User User { get; set; }
        public string userEmail { get; set; }
        //HomePage GetHomePage;
        public SignupPage()
        {

            InitializeComponent();  
            //User = new User { };

            BindingContext = viewModel = new SignUpViewModel();
            //BindingContext = this;

        }

        async void OnSave(object sender, EventArgs e)
        {
            if(viewModel.res == 0)
            {
                await DisplayAlert("Oups", "Un utilisateur avec cet email existe déjà...", "OK");
            }
            else
            {
                await DisplayAlert("Success", "Votre compte a été créé avec succès. ", "OK");
                await Navigation.PopToRootAsync();
            }
        }

        /*async void OnSave(object sender, EventArgs e)
        {
            //User.Email = this.userEmail;
            var userCourant = this.User;
            User userBase;
            using (MyDbContext context = new MyDbContext())
            {
                userBase = await context.Users.Where(u =>
             u.Email.Equals(userCourant.Email)).FirstOrDefaultAsync();


            }

            if (userBase != null)
            {
                await DisplayAlert("Oups", "An account with this email already exists.", "OK");
            }
            else
            {

               
                MessagingCenter.Send(this, "AddAccount", User);
                await DisplayAlert("Success", "Your account has been created", "OK");

                /* var HomePage = new HomePage();
                 HomePage.BindingContext = User.Email; // a voir pour la partie conservation de l'email dans le login
                 await Navigation.PushAsync(HomePage);*/
        //await Navigation.PopToRootAsync();

        // }

        // }
    }

}
