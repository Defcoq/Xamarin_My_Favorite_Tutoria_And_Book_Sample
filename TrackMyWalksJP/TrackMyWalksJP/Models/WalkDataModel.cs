using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TrackMyWalksJP.Models
{
    public class WalkDataModel
    {
        // [JsonProperty("id")]
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public double Distance { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
       
    }
}
