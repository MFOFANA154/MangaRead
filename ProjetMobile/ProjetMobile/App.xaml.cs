using ProjetMobile.Services;
using System;

using Xamarin.Forms;

namespace ProjetMobile
{

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MyDbContext.Init();
         

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());
        }
    }

   
}