using Microsoft.EntityFrameworkCore;
using ProjetMobile.Models;
using ProjetMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ProjetMobile.MockDataStoreManga))]
namespace ProjetMobile


{
    public class MockDataStoreManga : IDataStoreManga<Manga>
    {
        public int cpt = 0;
        List<Manga> mangas;

        public MockDataStoreManga()
        {
           
        }
        public async Task<bool> AddMangaAsync(Manga manga)
        {


            using (MyDbContext context = new MyDbContext())
            {
                manga.IdM = Guid.NewGuid().ToString();
                context.Mangas.Add(manga);

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);

        }

        public async Task<bool> UpdateMangaAsync(Manga manga)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _manga = await context.Mangas.Where((Manga arg) => arg.IdM == manga.IdM).FirstOrDefaultAsync();
                context.Remove(_manga);
                context.Mangas.Add(manga);

                await context.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteMangaAsync(string id)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _manga = await context.Mangas.Where((Manga arg) => arg.IdM == id).FirstOrDefaultAsync();
                context.Remove(_manga);

                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<Manga> GetMangaAsync(string id)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _manga = await context.Mangas.Where((Manga arg) => arg.IdM == id).FirstOrDefaultAsync();

                return _manga;
            }
        }

        public async Task<IEnumerable<Manga>> GetMangasAsync(bool forceRefresh = false)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var _manga = await context.Mangas.OrderBy(x => x.Titre).ToListAsync();

                return _manga;
            }
        }

    }

}