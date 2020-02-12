using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChroniusXF.DataModels;
using ChroniusXF.Utils;
using SQLite;

namespace ChroniusXF.Persistence
{
    public class ChroniusDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        public static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public ChroniusDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Chronius).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Chronius)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Chronius>> GetItemsAsync()
        {
            return Database.Table<Chronius>().ToListAsync();
        }

        public Task<List<Chronius>> GetActiveChroni()
        {
            return Database.QueryAsync<Chronius>("SELECT * FROM [Chronius] ORDER BY StartingDate DESC");
        }

        public Task<Chronius> GetChroniusAsync(int id)
        {
            return Database.Table<Chronius>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveChroniusAsync(Chronius chronius)
        {
            if (chronius.Id != 0)
                return Database.UpdateAsync(chronius);
            else
                return Database.InsertAsync(chronius);
        }

        public Task<int> DeleteChroniusAsync(Chronius chronius)
        {
            return Database.DeleteAsync(chronius);
        }
    }
}
