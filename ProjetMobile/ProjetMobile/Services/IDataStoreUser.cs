using ProjetMobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetMobile
{
    public interface IDataStoreUser<T>
    {
        Task<bool> AddUserAsync(T user);
        Task<bool> UpdateUserAsync(T user);
        Task<bool> AssignFavoriteToUserAsync(UserManga usermanga);
        Task<bool> DeleteFavoriteToUserAsync(UserManga usermanga);
        Task<bool> DeleteUserAsync(string id);
        Task<T> GetUserAsync(string id);
        Task<IEnumerable<T>> GetUsersAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetUsersEmailAsync(bool forceRefresh = false, string Email = "default");
        



    }
}