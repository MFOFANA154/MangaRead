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
    public partial class Description : ContentPage
    {
        public MangaDetailViewModel viewModel;
        public MangaDetailViewModel viewModel2;
        public Manga Manga { get; set; }
        public User User { get; set; }
        public UserManga UserManga { get; set; }

        public Description()
        {
            InitializeComponent();
            var manga = new Manga { };
            var user = new User { };
            viewModel = new MangaDetailViewModel(manga,user);
            BindingContext = viewModel;
        }
        public Description(MangaDetailViewModel viewModel, User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new MangaDetailViewModel(viewModel.Manga, user);
            User = user;
            Manga = viewModel.Manga;
            viewModel2 = viewModel;
        }

        async void OnAddFavorite(object sender, EventArgs e)
        {

            if (viewModel2.res == 0)
            {
                await DisplayAlert("Oups", "Ce manga est déjà dans votre liste de favoris.", "OK");
            }
            else
            {
                await DisplayAlert("Success", "Manga ajouté dans la liste des favoris.", "OK");

                await Navigation.PushAsync(new Recherche(new UserDetailViewModel(User)));

            }

            // MessagingCenter.Send(this, "UpdateFavorite", userManga);
            
            


        }

    }



}
