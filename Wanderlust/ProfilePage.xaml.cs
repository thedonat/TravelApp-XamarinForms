using System;
using System.Collections.Generic;
using SQLite;
using Wanderlust.Model;
using Xamarin.Forms;
using System.Linq;
using Plugin.DeviceOrientation;

namespace Wanderlust
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            

            var user = (await App.MobileService.GetTable<Users>().Where(u => u.Name == App.user.Name).ToListAsync()).FirstOrDefault(); //mail kayit kontrolu.

            var postTable = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();  //postlari list olarak aliyoruz.

            var categories = (from p in postTable orderby p.CategoryId select p.CategoryName).Distinct().ToList();


            Dictionary<string, int> categoriesCount = new Dictionary<string, int>() ;

            foreach(var category in categories) //her bir kategoriden kac tane girdi var.
            {
                var count = (from post in postTable where post.CategoryName == category select post).ToList().Count;

                categoriesCount.Add(category, count);
               
            }
            
            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = postTable.Count.ToString();

            usernameLabel.Text = user.Name + " " + user.Surname;


            if (user.Gender == "Male")
            {
                ppImage.Source = "touristboy.png";

            }
            else if (user.Gender == "Female")
            {
                ppImage.Source = "touristgirl.png";
            }
        
 
        }
    }
}
;