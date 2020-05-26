using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


using Foundation;
using UIKit;
using Microsoft.WindowsAzure.MobileServices;

namespace Wanderlust.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init(); //ios uygulamasinda maps calismasini sagliyoruz.
            CurrentPlatform.Init(); //azure servislerine baglanti

            string dbName = "travel_db.sqllite";
            string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"..","Library");
            string fullPath = Path.Combine(folderPath, dbName);

            LoadApplication(new App(fullPath));

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        
    }
}
