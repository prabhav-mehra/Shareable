using ShareAble.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareAble.Database
{
    public class LocalUsersDatabase
    {
        SQLiteAsyncConnection Database;

        public LocalUsersDatabase()
        {

        }

        async Task Init()
        {
            if (Database is not null)
            {
                Debug.WriteLine("exisit");
                return;

            }

            Database = new SQLiteAsyncConnection(LocalUsersConstants.DatabasePath, LocalUsersConstants.Flags);
            var result = await Database.CreateTableAsync<LocalUser>();
            Debug.WriteLine(result.ToString());
        }

        public async Task<List<LocalUser>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<LocalUser>().ToListAsync();
        }

        public async Task<LocalUser> GetItemAsync(int id)
        {
            
            await Init();
            Console.WriteLine("Id" + id);
            return await Database.Table<LocalUser>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<LocalUser> GetUserFromContact(long contactNumber)
        {
            return await Database.Table<LocalUser>().Where(i => i.ContactNumber == contactNumber).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(LocalUser item)
        {
            await Init();
            if (item.ID != 0)
            {
                Console.WriteLine("Update");
                return await Database.UpdateAsync(item);
            }

            else
            {
                Debug.WriteLine("New");
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(LocalUser item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        public async Task<int> DeleteAllItemAsync()
        {
            await Init();
          
             await Database.DeleteAllAsync<LocalUser>();
            return await Database.DropTableAsync<LocalUser>();
        }

    }
}