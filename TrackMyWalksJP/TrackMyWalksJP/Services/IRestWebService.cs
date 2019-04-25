using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Models;

namespace TrackMyWalksJP.Services
{
    public interface IRestWebService
    {
        // Gets all of the Walk Entries from our database.
        Task<List<WalkDataModel>> GetWalkEntries();

        // Saves our Walk Entry to the database.
        Task SaveWalkEntry(WalkDataModel item, bool isAdding);

        // Deletes a specific Walk Entry from the database.
        Task DeleteWalkEntry(string id);

        // Gets all of the Walk Entries from our database.
        Task<List<WalkDataModel>> GetWalkEntriesFirebase();

        // Saves our Walk Entry to the database.
        Task SaveWalkEntryFirebase(WalkDataModel item, bool isAdding);

        // Deletes a specific Walk Entry from the database.
        Task DeleteWalkEntryFirebase(int id);
    }
}
