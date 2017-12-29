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
	public partial class UserPage : ContentPage
	{
        public AddUserViewModel viewModel;
        public AddUserViewModel viewModel2;
        public User User { get; set; }
        public UserPage () //create
		{
			InitializeComponent (); 
            var user = new User { };
            viewModel = new AddUserViewModel();
            BindingContext = viewModel;
        }

        public UserPage(AddUserViewModel viewModel) //create
        {
            InitializeComponent(); //a
            //User = new User { };
            //viewModel = new UsersViewModel();
            BindingContext = viewModel = new AddUserViewModel(viewModel.User.Email);
            viewModel2 = viewModel;
        }



        async void OnCreate(object sender, EventArgs e)
        {
            if (viewModel2.res == 0)
            {
                await DisplayAlert("Oups", "Un utilisateur avec ce pseudo existe déja...", "OK");
            }
            else
            {
                await DisplayAlert("Success", "Votre utilisateur a été créé avec succès.", "OK");
                var accueil = new Accueil
                {
                    BindingContext = viewModel2
                };
                await Navigation.PushAsync(accueil);
            }

            //User.Email = this.userEmail;
            /*var userCourant = this.User;
            User userBase;
            using (MyDbContext context = new MyDbContext())
            {
                userBase = await context.Users.Where(u =>
             u.Email.Equals(userCourant.Email) && u.Pseudo.Equals(userCourant.Pseudo)).FirstOrDefaultAsync();


            }

            if (userBase != null)
            {
                
            }
            else
            {

                //User.Email = testEmail;
                MessagingCenter.Send(this, "AddUser", User);
                await DisplayAlert("Success", "Your user has been created.", "OK");

                /* var HomePage = new HomePage();
                 HomePage.BindingContext = User.Email; // a voir pour la partie conservation de l'email dans le login
                 await Navigation.PushAsync(HomePage);*/
               

            }

        }

       
    }
