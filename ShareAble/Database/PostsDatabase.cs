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
    public class PostsDatabase
    {
        SQLiteAsyncConnection Database;

        public PostsDatabase()
        {

        }

        async Task Init()
        {
            if (Database is not null)
            {
                Debug.WriteLine("exisit");
                return;

            }

            Database = new SQLiteAsyncConnection(PostsConstants.DatabasePath, PostsConstants.Flags);
            var result = await Database.CreateTableAsync<Posts>();
            Debug.WriteLine(result.ToString());
        }

        public async Task<List<Posts>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Posts>().ToListAsync();
        }

        public async Task<List<Posts>> GetUserPosts(int userId)
        {
            await Init();
            return await Database.Table<Posts>().Where(i => i.PartnerId == userId
            || i.UserId == userId).ToListAsync();
        }

        public async Task<List<Posts>> GetLocalUserPosts(int userId)
        {
            await Init();
            return await Database.Table<Posts>().Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Posts> GetItemAsync(int id)
        {

            await Init();
            Console.WriteLine("Id" + id);
            return await Database.Table<Posts>().Where(i => i.PictureId == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Posts item)
        {
            await Init();
            if (item.PictureId != 0)
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

        public async Task<int> DeleteItemAsync(Posts item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        public async Task<int> DeleteAllItemAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<Posts>();
        }

    }
}