using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wanderlust.Logic;
using Wanderlust.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wanderlust
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();
                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name,
                    UserId = App.user.Id,
                    Date = DateTime.Now.ToString("dd'/'MM'/'yyyy"),
                    Rate = IndustryTypePicker1.SelectedItem.ToString()
                   
            };

              

                await App.MobileService.GetTable<Post>().InsertAsync(post); //girilen yeni postu mobile servislere ekliyoruz.
                await DisplayAlert("Success", "Experience succesfully inserted", "Ok");
                await Navigation.PopAsync();
                
                

            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted", ex.Message);
                
            }
            
        }
    }
}