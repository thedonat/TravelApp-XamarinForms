using System;
using System.Collections.Generic;
using Wanderlust.Model;
using SQLite;

using Xamarin.Forms;

namespace Wanderlust
{
    public partial class PostDetailPage : ContentPage 
    {

        Post selectedPost; //list view dan gelen selectedPost property'sini global bir degiskene atayacagiz.


        public PostDetailPage(Post selectedPost)
        {

            InitializeComponent();

            this.selectedPost = selectedPost;

            experienceEntry.Text = selectedPost.Experience;
            venueLabel.Text = selectedPost.VenueName;
            categoryLabel.Text = selectedPost.CategoryName;
            addressLabel.Text = selectedPost.Address;
           
            
        }

        async void Update_Clicked(object sender, System.EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            await App.MobileService.GetTable<Post>().UpdateAsync(selectedPost); //girilen yeni postu mobile servislere ekliyoruz.
            await DisplayAlert("Success", "Experience succesfully updated", "Ok");
            await Navigation.PopAsync();


        }

        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            await App.MobileService.GetTable<Post>().DeleteAsync(selectedPost); //girilen yeni postu mobile servislere ekliyoruz.
            await DisplayAlert("Success", "Experience succesfully deleted", "Ok");
            await Navigation.PopAsync();
        }
    }
}
