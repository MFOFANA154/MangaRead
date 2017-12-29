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
	public partial class DetailFavorite : ContentPage
	{
        public MangaDetailViewModel viewModel;
        public MangaDetailViewModel viewModel2;
        public Manga Manga { get; set; }
        public User User { get; set; }
        public UserManga UserManga { get; set; }

        public DetailFavorite()
        {
            InitializeComponent();
            var manga = new Manga { };
            var user = new User { };
            viewModel = new MangaDetailViewModel(manga, user);
            BindingContext = viewModel;
        }
        public DetailFavorite(MangaDetailViewModel viewModel, User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new MangaDetailViewModel(viewModel.Manga, user);
            User = user;
            Manga = viewModel.Manga;
            viewModel2 = viewModel;
        }

        async void OnDeleteFavorite(object sender, EventArgs e)
        {
                await DisplayAlert("Success", "Manga supprimé de la liste des favoris.", "OK");

                await Navigation.PushAsync(new ListFavorite(new UserDetailViewModel(User)));


            // MessagingCenter.Send(this, "UpdateFavorite", userManga);




        }
    }
}