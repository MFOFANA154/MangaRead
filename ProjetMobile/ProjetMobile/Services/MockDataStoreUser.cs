using Microsoft.EntityFrameworkCore;
using ProjetMobile;
using ProjetMobile.Models;
using ProjetMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ProjetMobile.MockDataStoreUser))]
namespace ProjetMobile


{
    public class MockDataStoreUser : IDataStoreUser<User>
    {
        public int cpt = 0;      

        public MockDataStoreUser()
        {

         
        }
        public async Task<bool> AddUserAsync(User user)
        {

            using (MyDbContext context = new MyDbContext())
            {
                user.Id = Guid.NewGuid().ToString();
                context.Users.Add(user);            

                await context.SaveChangesAsync();
            }
          
            return await Task.FromResult(true);
        }
        
        public async Task<bool> UpdateUserAsync(User user)
        {


            using (MyDbContext context = new MyDbContext())
            {
                /*var _user = await context.Users.Where((User arg) => arg.Id == user.Id).FirstOrDefaultAsync();
                context.Remove(_user);
                
                await context.SaveChangesAsync();
                */
                context.Users.Update(user);

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }


        public async Task<bool> AssignFavoriteToUserAsync(UserManga usermanga)
        {

            using (MyDbContext context = new MyDbContext())
            {
                var _user = await context.Users.Where(arg => arg.Id == usermanga.UserId).FirstOrDefaultAsync();
                var _manga = await context.Mangas.Where( arg => arg.IdM == usermanga.MangaIdM).FirstOrDefaultAsync();

                _user.Favoris.Add(usermanga);
                usermanga.Id = Guid.NewGuid().ToString();

                context.UserMangas.Add(usermanga);

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteFavoriteToUserAsync(UserManga usermanga)
        {

            using (MyDbContext context = new MyDbContext())
            {
                var _user = await context.Users.Where(arg => arg.Id == usermanga.UserId).FirstOrDefaultAsync();
                var _manga = await context.Mangas.Where(arg => arg.IdM == usermanga.MangaIdM).FirstOrDefaultAsync();

                context.UserMangas.Remove(usermanga);

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {

            using (MyDbContext context = new MyDbContext())
            {
                var _user = await context.Users.Where((User arg) => arg.Id == id).FirstOrDefaultAsync();
                var _usermangas = await context.UserMangas.Where(arg => arg.UserId == id).ToListAsync();
                foreach(var usermanga in _usermangas)
                {
                    context.UserMangas.Remove(usermanga);
                }


                context.Remove(_user);
                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<User> GetUserAsync(string id)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _user = await context.Users.Where((User arg) => arg.Id == id).FirstOrDefaultAsync();

                return _user;
            }
        }
           

        public async Task<IEnumerable<User>> GetUsersAsync(bool forceRefresh = false)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _user = await context.Users.ToListAsync();

                return _user;
            }
          
        }

        public async Task<IEnumerable<User>> GetUsersEmailAsync(bool forceRefresh = false, string Email = "default")
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _user = await context.Users.Where(u => u.Email.Equals(Email)).ToListAsync();

                return _user;
            }
           
        }

       

    }

}

