using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Wanderlust
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender , EventArgs e)
        {
            Navigation.PushAsync(new NewTravelPage());
        }
    }
}
