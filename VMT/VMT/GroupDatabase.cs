using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace VMT
{
    public class GroupDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public GroupDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Group>().Wait();
        }

        public Task<List<Group>> GetGroupsAsync()
        {
            return _database.Table<Group>().ToListAsync();
        }

        public Task<Group> GetGroupAsync(int id)
        {
            return _database.Table<Group>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveGroupAsync(Group group)
        {
            if (group.ID != 0)
            {
                return _database.UpdateAsync(group);
            }
            else
            {
                return _database.InsertAsync(group);
            }
        }

        public Task<int> DeleteGroupAsync(Group group)
        {
            return _database.DeleteAsync(group);
        }
    }
}