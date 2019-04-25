using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalksJP.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using MoreLinq;

namespace TrackMyWalksJP.Services
{
   public  class RestWebService : IRestWebService
    {
        // Declare our HttpClient manager object
        HttpClient client;
        FirebaseClient firebase;


        // Declare our RestWebService Constructor
        public RestWebService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://trackmywalk.azurewebsites.net");
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

            firebase = new FirebaseClient("https://trackmywalks-4a0f0.firebaseio.com//");
        }

        // Retrieves all of the Walk Entries from our database.
        public async Task<List<WalkDataModel>> GetWalkEntries()
        {
            // Declare our WalkEntries Items List Collection to populate resultset
            var Items = new List<WalkDataModel>();
            try
            {
                var response = await client.GetAsync("tables/WalkEntries");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<WalkDataModel>>(content);
                }
            }
            catch (Exception ex)
            {
                // Catch and output any error messages that have occurred
                Debug.WriteLine("An error occurred {0}", ex.Message);
            }
            return Items;
        }

        // Saves the Walk Entry item that is currently being added/edited.
        public async Task SaveWalkEntry(WalkDataModel item, bool isAdding)
        {
            try
            {
                HttpResponseMessage responseMessage;
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Check to see if we are adding or editing, handle accordingly.
                if (isAdding)
                {
                    responseMessage = await client.PostAsync("tables/WalkEntries", content);
                }
                else
                {
                    responseMessage = await client.PutAsync("tables/WalkEntries", content);
                }
                // Check to see if we have successfully written the item to the database
                if (responseMessage.IsSuccessStatusCode)
                {
                    Debug.WriteLine("WalkEntry Item successfully saved.");
                }
            }
            catch (Exception ex)
            {
                // Catch and output any error messages that have occurred
                Debug.WriteLine("An error occurred {0}", ex.Message);
            }
        }

        // Deletes a specific Walk Entry from the database using the id.
        public async Task DeleteWalkEntry(string id)
        {
            try
            {
                var response = await client.DeleteAsync("/tables/WalkEntries/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("WalkEntry Item was successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                // Catch and output any error messages that have occurred
                Debug.WriteLine("An error occurred {0}", ex.Message);
            }
        }

        public async Task<List<WalkDataModel>> GetWalkEntriesFirebase()
        {
            var Items = new List<WalkDataModel>();
            try
            {
                //var item1 = new WalkDataModel
                //{
                //    Id = 1,
                //    Title = "10 Mile Brook Trail, Margaret River",
                //    Description = "The 10 Mile Brook Trail starts in the Rotary Park near Old Kate, a preserved steam engine at the northern edge of Margaret River. ",
                //    Latitude = -33.9727604,
                //    Longitude =115.0861599,
                //    Distance = 7.5,
                //    Difficulty = "Medium",
                //    ImageUrl = "http://trailswa.com.au/media/cache/media/images/trails/_mid/FullSizeRender1_600_480_c1.jpg"
                //};
                //await firebase
                //    .Child("WalkDataModels")
                //    .PostAsync(item1);


                var path =  firebase.Child("WalkDataModels").OnceAsync<WalkDataModel> ();
                if(path != null)
                {
                    Items = (await path).Select(item => new WalkDataModel
                    {
                        Description = item.Object.Description,
                        Difficulty = item.Object.Difficulty,
                        Distance = item.Object.Distance,
                        Id =item.Object.Id,
                        ImageUrl = item.Object.ImageUrl,
                        Latitude =item.Object.Latitude,
                        Longitude =item.Object.Longitude,
                        Title = item.Object.Title
                        
                    }).ToList();

                }

             
               
            }
            catch (Exception ex)
            {

                // Catch and output any error messages that have occurred
                Debug.WriteLine("An error occurred {0}", ex.Message);
            }
            return Items;

        }

        public async Task SaveWalkEntryFirebase(WalkDataModel item, bool isAdding)
        {
            if(isAdding)
            {
                var allWalkDataModels = await GetWalkEntriesFirebase();
                await firebase
                  .Child("WalkDataModels")
                  .OnceAsync<WalkDataModel>();
               
                if(allWalkDataModels.Count > 0)
                {
                    var model = allWalkDataModels.MaxBy(a => a.Id).FirstOrDefault();
                    int next = model.Id + 1;
                    item.Id = next;
                }
                else
                {
                    item.Id = 1;
                }
                await firebase
                      .Child("WalkDataModels")
                      .PostAsync(item);
            }
            else
            {
                var toUpdateWalkDataModel = (await firebase
             .Child("WalkDataModels")
             .OnceAsync<WalkDataModel>()).Where(a => a.Object.Id == item.Id).FirstOrDefault();

                await firebase
                  .Child("WalkDataModels")
                  .Child(toUpdateWalkDataModel.Key)
                  .PutAsync(item);
            }
         
        }

        public async Task DeleteWalkEntryFirebase(int id)
        {
            var toDeleteWalkDataModel = (await firebase
              .Child("WalkDataModels")
              .OnceAsync<WalkDataModel>()).Where(a => a.Object.Id== id).FirstOrDefault();
            await firebase.Child("WalkDataModels").Child(toDeleteWalkDataModel.Key).DeleteAsync();
        }
    }
}
