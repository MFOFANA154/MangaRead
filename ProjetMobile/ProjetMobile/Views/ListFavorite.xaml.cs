using ProjetMobile.Models;
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
	public partial class ListFavorite : ContentPage
	{

        MangasViewModel mangasViewModel;
        public UserDetailViewModel viewModel2 { get; set; }
        public User User { get; set; }
        public Manga Manga { get; set; }
        public ListFavorite()
        {
            InitializeComponent();

            BindingContext = mangasViewModel = new MangasViewModel(User);
            favoritelist.ItemsSource = mangasViewModel.Mangas;
        }

        public ListFavorite(UserDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel = new UserDetailViewModel(viewModel.User);
            BindingContext = mangasViewModel = new MangasViewModel(viewModel.User);
            favoritelist.ItemsSource = mangasViewModel.Mangas;
            

            viewModel2 = viewModel;
            //var manga5 = new Manga { };

        }

        async void OnFavoriteSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var manga = args.SelectedItem as Manga;
            if (manga == null)
                return;

            // await Navigation.PushAsync(new Recherche());
            await Navigation.PushAsync(new DetailFavorite(new MangaDetailViewModel(manga), viewModel2.User));


            // Manually deselect item
            favoritelist.SelectedItem = null;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (mangasViewModel.Mangas.Count == 0)
                mangasViewModel.LoadFavoriteCommand.Execute(null);
        }
    }
}