using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.DeviceOrientation;
using Wanderlust.Model;
using Xamarin.Forms;

namespace Wanderlust
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();

           
        }

        private async void loginButtonClicked(object sender, EventArgs e)
        {


            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if(isEmailEmpty || isPasswordEmpty)
            {
                await DisplayAlert("Error", "Please enter your email and password", "OK");
            }
            else
            {
                var user = (await App.MobileService.GetTable<Users>().Where(u => u.Email == emailEntry.Text).ToListAsync()).FirstOrDefault(); //mail kayit kontrolu.

                if (user != null) //daha once herhangi bir kayit var mi ?
                {
                    App.user = user;

                    if(user.Password == passwordEntry.Text) //passoword controlu
                        await Navigation.PushAsync(new HomePage());
                    else
                       await DisplayAlert("Error","Email or password are incorrect" , "OK");
                }

                else
                {
                     await DisplayAlert("Error", "User can not be found", "OK");

                }
            }
        }

        private void registerUserButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

    }
}
