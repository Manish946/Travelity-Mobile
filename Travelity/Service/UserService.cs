using PropertyChanged;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Travelity.Abstractions.Models;
using Travelity.Models;
using Travelity.Service;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(UserService))]
namespace Travelity.Service
{
    [AddINotifyPropertyChangedInterface]

    public class UserService:IUserService
    {
        static SQLiteAsyncConnection LocalDb;

        static async Task init()
        {
            // If db already created do not create again.
            if (LocalDb != null)
            {
                return;

            }
            else
            {
                // Get an absolute path to the database file
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

                LocalDb = new SQLiteAsyncConnection(databasePath);
                await LocalDb.CreateTableAsync<User>();
                await LocalDb.CreateTableAsync<GoogleUser>();
            }

           
        }

        public  async Task AddCurrentUser(User user)
        {
            await init();
            User CurrentUser = user;
            var id = await LocalDb.InsertAsync(CurrentUser);
        }
        public  async Task AddCurrentGoogleUser(GoogleUser user)
        {
            await init();
            GoogleUser CurrentGoogleUser = user;
            var id = await LocalDb.InsertAsync(CurrentGoogleUser);
        }

        public  async Task RemoveCurrentUser()
        {
            await init();
            await LocalDb.DeleteAllAsync<User>();
        }

        public  async Task<User> GetCurrentUser()
        {
            await init();
            var CurrentUser = await LocalDb.Table<User>().FirstOrDefaultAsync();
            return CurrentUser;
        }
        public  async Task<GoogleUser> GetCurrentGoogleUser()
        {
            await init();
            var CurrentUser = await LocalDb.Table<GoogleUser>().FirstOrDefaultAsync();
            return CurrentUser;
        }

        public async Task RemoveCurrentGoogleUser()
        {
            await init();
            await LocalDb.DeleteAllAsync<GoogleUser>();
        }

        public async Task updateCurrentUser(User user)
        {
            await init();
            await RemoveCurrentUser();
            User CurrentUser = user;
            var id = await LocalDb.InsertAsync(CurrentUser);
        }
    }
}
