using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChroniusXF.DataModels;
using SQLite;

namespace ChroniusXF.Persistence
{
    public interface IChroniusDatabase
    {
        SQLiteAsyncConnection Database { get; }
        Task<List<Chronius>> GetItemsAsync();
        Task<List<Chronius>> GetActiveChroni();
        Task<Chronius> GetChroniusAsync(int id);
        Task<int> SaveChroniusAsync(Chronius chronius);
        Task<int> DeleteChroniusAsync(Chronius chronius);
    }
}
