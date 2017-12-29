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
    public partial class Recherche : ContentPage
    {
        UserDetailViewModel viewModel;
        public UserDetailViewModel viewModel2 { get; set; }
        MangasViewModel mangasViewModel;
        public MangasViewModel mangasViewModel2 { get; set; }
        MangaDetailViewModel mangaDetailViewModel;
        public User User { get; set; }
        public Manga Manga { get; set; }
        public List<Manga> tempdata;


        public Recherche()
        {
            InitializeComponent();
            var user = new User { };
            var manga = new Manga { };
            viewModel = new UserDetailViewModel(user);
            mangaDetailViewModel = new MangaDetailViewModel(manga);
            BindingContext = viewModel;
            BindingContext = mangasViewModel = new MangasViewModel();
        }
        public Recherche(UserDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = mangasViewModel = new MangasViewModel();
            mangalist.ItemsSource = mangasViewModel.Mangas;
            BindingContext = viewModel = new UserDetailViewModel(viewModel.User);
            viewModel2 = viewModel;
            //var manga5 = new Manga { };


        }

        async void OnMangaSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var manga = args.SelectedItem as Manga;
            if (manga == null)
                return;

            // await Navigation.PushAsync(new Recherche());
            await Navigation.PushAsync(new Description(new MangaDetailViewModel(manga),viewModel2.User));

            // Manually deselect item
            mangalist.SelectedItem = null;
        }


        async void OnEditUser(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new UserPageUPD(new UserDetailViewModel(viewModel2.User)));
        }

        async void OnFavoriteList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListFavorite(new UserDetailViewModel(viewModel2.User)));
        }

        async void OnDeleteUser(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "L'utilisateur a été éffacé avec succès.", "OK");
            var accueil = new Accueil
            {
                BindingContext = viewModel2
            };
            await Navigation.PushAsync(accueil);
        }

        /*public List<Manga> Data()
        {
            // all the object data  
            
            
           
              return (realm.All<Manga>().ToList()); // remplacer ca par une command listview,

        }*/

        private void OnChangeText(object sender, TextChangedEventArgs e)
        {
            //thats all you need to make a search  

            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                mangalist.ItemsSource = mangasViewModel.Mangas;
            }

            else
            {
                mangalist.ItemsSource = mangasViewModel.Mangas.Where(x => x.Titre.StartsWith(e.NewTextValue));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (mangasViewModel.Mangas.Count == 0)
                mangasViewModel.LoadMangasCommand.Execute(null);
        }
    }
}