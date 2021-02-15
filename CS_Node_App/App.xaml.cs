using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CS_Node_App.Services;
using CS_Node_App.Views;

namespace CS_Node_App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
