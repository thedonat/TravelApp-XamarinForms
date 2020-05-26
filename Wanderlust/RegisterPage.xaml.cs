using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wanderlust.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wanderlust
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                // We can register the user
                Users user = new Users()
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text,
                    Name = nameEntry.Text,
                    Surname = surnameEntry.Text,
                    Gender = genderEntry.Text
                    
                };

                await App.MobileService.GetTable<Users>().InsertAsync(user);
                await DisplayAlert("Successfull", "You are successfully registered", "OK");
                await Navigation.PopAsync();

            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "OK");
            }
        }
    }
}