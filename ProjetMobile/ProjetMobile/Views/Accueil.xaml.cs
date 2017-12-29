using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjetMobile.Models;
using ProjetMobile.ViewModels;

namespace ProjetMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Accueil : ContentPage
	{
        UsersViewModel viewModel;
        public User User { get; set; }
        public string testEmail { get; set; }
        

        public Accueil ()
		{
			InitializeComponent();

            BindingContext = viewModel = new UsersViewModel();
            UsersListView.ItemsSource = viewModel.Users;
            
        }

        async void OnUserSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var user = args.SelectedItem as User;
            if (user == null)
                return;

           // await Navigation.PushAsync(new Recherche());
            await Navigation.PushAsync(new Recherche(new UserDetailViewModel(user)));

            

            // Manually deselect item
            UsersListView.SelectedItem = null;
        }

        async void OnAddUser(object sender, EventArgs e)
        {

            testEmail = testemail.Text.ToString();
            await Navigation.PushAsync(new UserPage(new AddUserViewModel(testEmail)));
        }

        async void OnLogOut(object sender, EventArgs e)
        {

            await Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            testEmail = testemail.Text;
            viewModel.BindingEmail = testEmail; // on récupère l'email correspondant au compté logé et on l'envoie a la viewModel
            if (viewModel.Users.Count == 0)
                viewModel.LoadPseudosCommand.Execute(null);

            
        }
    }
}