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
	public partial class UserPageUPD : ContentPage
	{
        public UserDetailViewModel viewModel { get; set; }
        public UserDetailViewModel viewModel2 { get; set; }
        public UsersViewModel usersViewModel;
        public User User { get; set; }
        public UserPageUPD ()
		{
            InitializeComponent();
            var user = new User { };
            viewModel = new UserDetailViewModel(user);
            BindingContext = viewModel;
        }

        public  UserPageUPD(UserDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel = new UserDetailViewModel(viewModel.User) ; // update
            viewModel2 = viewModel;

        }

        async void OnUpdate(object sender, EventArgs e)
        {
            
            if (viewModel2.res == 0)
            {
                await DisplayAlert("Oups", "Un utilisateur avec ce pseudo existe déjà.", "OK");
            }
            else
            {
                await DisplayAlert("Success", "Votre utilisateur a été mis à jour avec succès.", "OK");
                var accueil = new Accueil
                {
                    BindingContext = viewModel2
                };
                await Navigation.PushAsync(accueil);
            }

            /*
            User userBase;
            using (MyDbContext context = new MyDbContext())
            {
                userBase = await context.Users.Where(u =>
            u.Pseudo.Equals(pseudo.Text.ToString()) && u.Email.Equals(email.Text.ToString())).FirstOrDefaultAsync();
            }
            
            if (userBase != null)
            {
                await DisplayAlert("Oups", "An user with this username already exists.", "OK");
            }
            else
            {
                var UPD = new User
                {
                    Id = this.viewModel.User.Id,
                    Pseudo = pseudo.Text.ToString(),
                    FirstName = firstname.Text.ToString(),
                    LastName = lastname.Text.ToString(),
                    Email = email.Text.ToString(),
                    Password = null,
                    DateTime = datetime.Date,

                };
                
                MessagingCenter.Send(this, "UpdateUser", UPD);
                await DisplayAlert("Success", "Your user has been updated.\nYou will be disconnected for the update", "OK");*/

                /* var HomePage = new HomePage();
                 HomePage.BindingContext = User.Email; // a voir pour la partie conservation de l'email dans le login
                 await Navigation.PushAsync(HomePage);*/
               /* var accueil = new Accueil
                {
                    BindingContext = viewModel
                };
                await Navigation.PushAsync(accueil);*/
     
            }

        }
    }
