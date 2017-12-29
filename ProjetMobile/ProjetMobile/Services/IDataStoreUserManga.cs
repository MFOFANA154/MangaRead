using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMobile.Services
{
    public interface IDataStoreUserManga<T>
    {
        Task<IEnumerable<T>> GetMangasFavoriteAsync(string id = "");
    }
    
}
