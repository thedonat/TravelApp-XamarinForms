using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Wanderlust.Logic;
using Wanderlust.Model;
using Xamarin.Forms;

namespace Wanderlust
{
    public partial class ExpolererPage : ContentPage
    {
        public ExpolererPage()
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

        private void ListTapped(object sender, ItemTappedEventArgs e)
    {

            var tappedPost = venueListView.SelectedItem as Venue;
           

            Navigation.PushAsync(new ExploreDetailPage(tappedPost)); //ExploreDateilPage e parametre olarak gonderiyoruz.


    }   
    }
}
