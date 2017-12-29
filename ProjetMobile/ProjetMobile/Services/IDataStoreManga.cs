using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMobile.Services
{
    public interface IDataStoreManga<T>
    {
        Task<bool> AddMangaAsync(T user);
        Task<bool> UpdateMangaAsync(T user);
        Task<bool> DeleteMangaAsync(string id);
        Task<T> GetMangaAsync(string id);
        Task<IEnumerable<T>> GetMangasAsync(bool forceRefresh = false);

    }
}
