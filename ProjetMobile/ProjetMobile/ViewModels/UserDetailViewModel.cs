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
    public class UserDetailViewModel : BaseViewModel
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


    
        private UserDetailViewModel viewModel;

        public User User { get; set; }
        public int res { get; set; }
        public UserDetailViewModel(User user = null)
        {
            Title = user?.Pseudo;
            User = user;

            UpdateClickedCommand = new Command(async () => await OnUpdate());
            DeleteClickedCommand = new Command(async () => await OnDelete());

        }

        public UserDetailViewModel(UserDetailViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public ICommand UpdateClickedCommand { get; private set; }
        public ICommand DeleteClickedCommand { get; private set; }

        public async Task OnUpdate()
        {
            var userCourant = User;
            User userBase;
            try
            {
                using (MyDbContext context = new MyDbContext())
                {
                    userBase = await context.Users.Where(u =>
                u.Pseudo.Equals(userCourant.Pseudo) && u.Email.Equals(userCourant.Email)).FirstOrDefaultAsync();
                }

                if (userBase != null)
                {
                    res = 0;
                }
                else
                {
                    res = 1;
                    await DataStoreUser.UpdateUserAsync(User);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

        }

        public async Task OnDelete()
        {

            try
            {
                await DataStoreUser.DeleteUserAsync(User.Id);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
    }
}
