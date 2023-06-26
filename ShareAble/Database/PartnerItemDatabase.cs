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
    public class PartnerItemDatabase
    {
        SQLiteAsyncConnection Database;

        public PartnerItemDatabase()
        {

        }

        async Task Init()
        {
            if (Database is not null)
            {
                Debug.WriteLine("exisit");
                return;

            }

            Database = new SQLiteAsyncConnection(UsersConstants.DatabasePath, UsersConstants.Flags);
            var result = await Database.CreateTableAsync<Partner>();
            Debug.WriteLine(result.ToString());
        }

        public async Task<List<Partner>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Partner>().ToListAsync();
        }

        public async Task<Partner> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Partner>().Where(i => i.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Partner item)
        {
            await Init();
            if (item.PartnerId != 0)
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

        public async Task<int> DeleteItemAsync(Partner item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        public async Task<int> DeleteAllItemAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<Partner>();
        }

        public async Task AddPartnerAsync(int userId, int partnerUserId)
        {
            await Init();
            var existingPartner = await Database.Table<Partner>()
        .Where(p => (p.UserId == userId && p.PartnerUserId == partnerUserId) ||
                    (p.UserId == partnerUserId && p.PartnerUserId == userId))
        .FirstOrDefaultAsync();

            if (existingPartner != null)
            {
                // Partnership already exists, no need to add a new record
                return;
            }

            // If the partnership does not exist, insert a new record
            var partner = new Partner
            {
                PartnerId = 0,
                UserId = userId,
                PartnerUserId = partnerUserId
            };

            await SaveItemAsync(partner);
        }
    }
}