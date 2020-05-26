using System;
using System.Collections.Generic;


using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator.Abstractions;
using Wanderlust.Model;
using SQLite;
using Xamarin.Forms.Maps;

namespace Wanderlust


{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MapPage : ContentPage
    {


        public MapPage()
        {
            InitializeComponent();

        }



        protected override async void OnAppearing() //Sayfa her acildiginda kullnicinin bulundugu yeri anlik otomatik almak.
        {
            base.OnAppearing();


                var locator = CrossGeolocator.Current;

                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);  //location en az 100 metre degistiginde guncelle.
            

            GetLocation();

            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();
            DisplayInMap(posts);
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {

                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address,
                        

                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (Exception)
                {


                }

            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;

        }



        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            

                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(); //user location almak

                MoveMap(position);
            
        }

        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude); //koordinatlari aliyoruz.ekranin merkezinde olacak.
            var span = new MapSpan(center, 0.1,0.1); //sayisal degerler haritanin ne kadar uzaktan acilmasi gerektigini belirtir.
            locationsMap.MoveToRegion(span); //istenilen bolgeye haritanin gitmesini sagliyoruz.
        }
    }
}


