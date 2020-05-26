using System;
using System.Collections.Generic;
using Wanderlust.Model;
using Wanderlust.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wanderlust.Logic
{
    public class VenueLogic
    {
         public async static Task<List<Venue>>  GetVenues(double latitude, double longitude)
        {
            List<Venue> venues = new List<Venue>();


            var url = VenueRoot.GenerateURL(latitude, longitude);

            using (HttpClient client = new HttpClient()) //HTTP REQUEST
            {
               var response = await client.GetAsync(url);
               var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json); //deserializing JSON

                venues = venueRoot.response.venues as List<Venue>;
            }

                return venues; 
        }
    }
}
