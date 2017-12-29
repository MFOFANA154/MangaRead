using ProjetMobile.Models;
using ProjetMobile.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjetMobile.ViewModels
{
    public class SignUpViewModel : BaseViewModel
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

        private string _Password;
        public string Password
        { get { return _Password; } set { SetProperty(ref _Password, value); } }

        private DateTimeOffset _DateTime;
        public DateTimeOffset DateTime
        { get { return _DateTime; } set { SetProperty(ref _DateTime, value); } }

        public User User { get; set; }
        public int res { get; set; }

        public SignUpViewModel()
    {
            User = new User { };
            SaveClickedCommand = new Command(async () => await Save_Clicked());


    }

        public ICommand SaveClickedCommand { get; private set; }
        
        

        public async Task Save_Clicked()
        {
            var userCourant = User;
            User userBase;
            try
            {
              
                using (MyDbContext context = new MyDbContext())
                {
                    userBase = await context.Users.Where(u =>
                 u.Email.Equals(userCourant.Email)).FirstOrDefaultAsync();
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
