using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VMT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameSelectionPage : ContentPage
    {
        public GameSelectionPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetGroupsAsync();
        }

        async void OnListViewGroupSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            if (e.SelectedItem != null && (e.SelectedItem as Group).Words.Length>0)
            {
                await Navigation.PushAsync(new GamePage(e.SelectedItem as Group)
                {

                    BindingContext = e.SelectedItem as Group
                });
            }
            else
            {
                await DisplayAlert("", "Cannot Practice Empty Group", "OK");
                listView.SelectedItem = null;
                return;
            }
        }
    }
}