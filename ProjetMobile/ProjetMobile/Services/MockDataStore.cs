using ProjetMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ProjetMobile.MockDataStore))]
namespace ProjetMobile
{

    public class MockDataStore : IDataStore<Item>
    {
       




        public MockDataStore()
        {
           
           
            


        }

        public async Task<bool> AddItemAsync(Item item)
        {

          
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
           

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(String id)
        {
           

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {

            return await Task.FromResult(new List<Item>().FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(new List<Item>().ToList());
        }


    }
}
