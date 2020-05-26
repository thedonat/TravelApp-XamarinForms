using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;

using Xamarin.Forms;
using Wanderlust.Model;

namespace Wanderlust
{
    public partial class HistoryPage : ContentPage
    {


        public HistoryPage()
        {
            InitializeComponent();
        }

        

        protected override async void OnAppearing() //History Page'e her dondugumuzde veriler guncellenecek.
        {
            base.OnAppearing();


            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();


            //girilen postu userid ye gore alip history page'e aktarma.
            postListView.ItemsSource = posts; //list view e olusturulan postlari atiyoruz.
        }

        void Handle_ItemSelected(object sender , Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

            var selectedPost = postListView.SelectedItem as Post;

            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost)); 
            }
        }

        async void SearchBar_TextChanged(object sender , TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

                postListView.ItemsSource = posts.Where(x => x.VenueName.Contains(e.NewTextValue)); ;
            }

            else
            {
                var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

                postListView.ItemsSource = posts;
            }
        }
    }
}
