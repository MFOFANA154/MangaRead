using Microsoft.EntityFrameworkCore;
using ProjetMobile.Models;
using ProjetMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ProjetMobile.MockDataStoreUserManga))]
namespace ProjetMobile
{
   
    public class MockDataStoreUserManga : IDataStoreUserManga<UserManga>
    {

        List<UserManga> usermangas;

        public MockDataStoreUserManga()
        {

        }
        public async Task<IEnumerable<UserManga>> GetMangasFavoriteAsync(string id)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _usermanga = await context.UserMangas.Where(u => u.UserId == id).ToListAsync();
                return _usermanga;
            }
        }
    }
}
