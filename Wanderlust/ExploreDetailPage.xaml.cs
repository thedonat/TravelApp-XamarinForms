using System;
using System.Collections.Generic;
using Wanderlust.Model;
using Xamarin.Forms;

namespace Wanderlust
{
    public partial class ExploreDetailPage : ContentPage
    {
        private Venue tappedPost;


        public ExploreDetailPage(Venue tappedPost)
        {

            InitializeComponent();

            this.tappedPost = tappedPost;
            venueName.Text = tappedPost.name;
            

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var review = await App.MobileService.GetTable<Post>().Where(p => p.VenueName == tappedPost.name).ToListAsync();

            reviewListView.ItemsSource = review;

        }
    }
}
