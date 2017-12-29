using Microsoft.EntityFrameworkCore;
using ProjetMobile.Models;
using ProjetMobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjetMobile.ViewModels
{
    public class AddUserViewModel : BaseViewModel
    {
        private string _Pseudo;
        public string Pseudo
        { get { return _Pseudo; } set { SetProperty(ref _Pseudo, value); } }

        private string _FirstName;
        public string FirstName
        { get { return _FirstName; } set { SetProperty(ref _FirstName, value); } }

        private string _LastName;
        public string LastName
        { get { return _LastName; } set { SetProperty(ref _LastName, value); } }

        private string _Email;
        public string Email
        { get { return _Email; } set { SetProperty(ref _Email, value); } }


        public User User { get; set; }
        public int res { get; set; }

        public AddUserViewModel(string email)
        {
            User = new User { };
            User.Email = email;
            CreateClickedCommand = new Command(async () => await OnCreate());


        }

        public AddUserViewModel()
        {
        }

        public ICommand CreateClickedCommand { get; private set; }
        


        public async Task OnCreate()
        {
            var userCourant = User;
            User userBase;
            try
            {

                using (MyDbContext context = new MyDbContext())
                {
                    userBase = await context.Users.Where(u =>
                 u.Email.Equals(userCourant.Email) && u.Pseudo.Equals(userCourant.Pseudo)).FirstOrDefaultAsync();


                }

                if (userBase != null)
                {

                    res = 0;

                }
                else
                {
                    res = 1;

                    // MessagingCenter.Send(this, "AddAccount", User);
                    await DataStoreUser.AddUserAsync(User);


                    /* var HomePage = new HomePage();
                     HomePage.BindingContext = User.Email; // a voir pour la partie conservation de l'email dans le login
                     await Navigation.PushAsync(HomePage);*/



                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

        }


    }
}
