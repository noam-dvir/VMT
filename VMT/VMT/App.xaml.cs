using System;
using System.IO;
using Xamarin.Forms;

namespace VMT
{
    public partial class App : Application
    {
        static GroupDatabase database;

        public static GroupDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new GroupDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Groups.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new GroupsPage());
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
