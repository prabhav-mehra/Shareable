using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ShareAble.Database
{
    public class UsersItemDatabase
    {
        SQLiteAsyncConnection Database;

        public UsersItemDatabase()
        {

        }

        async Task Init()
        {
            if (Database is not null)
            {
                Debug.WriteLine("exisit");
                return;

            }

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<UsersModel>();
            Debug.WriteLine(result.ToString());
        }

        public async Task<List<UsersModel>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<UsersModel>().ToListAsync();
        }

        //public async Task<List<UsersModel>> GetItemsNotDoneAsync()
        //{
        //    await Init();
        //    return await Database.Table<UsersModel>().Where(t => t.Done).ToListAsync();

        //    // SQL queries are also possible
        //    //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public async Task<UsersModel> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<UsersModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(UsersModel item)
        {
            await Init();
            if (item.ID != 0)

                return await Database.UpdateAsync(item);
            else
            {
                Debug.WriteLine("New");
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(UsersModel item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

    }
}