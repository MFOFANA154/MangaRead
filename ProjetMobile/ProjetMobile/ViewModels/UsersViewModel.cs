using Microsoft.EntityFrameworkCore;
using ProjetMobile.Models;
using ProjetMobile.Services;
using ProjetMobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjetMobile
{
    public class UsersViewModel : BaseViewModel
    {
        

        public ObservableCollection<User> Users { get; set; }
        public Command LoadUsersCommand { get; set; }
        public Command LoadPseudosCommand { get; set; }
        public String BindingEmail { get; set; }
        public int cpt = 0;
        public UsersViewModel()
        {
            Title = "Accueil";
            Users = new ObservableCollection<User>();
            LoadUsersCommand = new Command(async () => await ExecuteLoadUsersCommand());
            
            LoadPseudosCommand = new Command(async () => await ExecuteLoadPseudosCommand());

            


            /*MessagingCenter.Subscribe<SignupPage, User>(this, "AddAccount", async (obj, user) =>
            {
                var _user = user as User;
                Users.Add(_user);
                await DataStoreUser.AddUserAsync(_user);
            });*/

            MessagingCenter.Subscribe<UserPage, User>(this, "AddUser", async (obj, user) =>
            {
                var _user = user as User;
                Users.Add(_user);
                await DataStoreUser.AddUserAsync(_user);
                    
            });

            MessagingCenter.Subscribe<UserPageUPD, User>(this, "UpdateUser", async (obj, user) =>
            {

                if (cpt < 1)
                {
                    cpt++;
                    var _user = user as User;
                    await DataStoreUser.UpdateUserAsync(_user);
                }
                
            });


            MessagingCenter.Subscribe<Recherche, User>(this, "DeleteUser", async (obj, user) =>
            {
                var _user = user as User;
                Users.Add(_user);
                await DataStoreUser.DeleteUserAsync(_user.Id);

            });

            MessagingCenter.Subscribe<Description, UserManga>(this, "UpdateFavorite", async (obj, usermanga) =>
            {
                var _usermanga = usermanga as UserManga;
                await DataStoreUser.AssignFavoriteToUserAsync(_usermanga);

            });
        }

        


        async Task ExecuteLoadUsersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                Users.Clear();
                var users = await DataStoreUser.GetUsersAsync(true);
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadPseudosCommand() // TODO créer une commande qui chargera la liste de tous les pseudos en fonction de l'email du login
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                Users.Clear();
                //BindingEmail = "mfofana154@gmail.com";
                var users = await DataStoreUser.GetUsersEmailAsync(true,BindingEmail); // on filtre la liste de tous les comptes lié ac l'adreese
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

