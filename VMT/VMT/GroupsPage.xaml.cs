using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace VMT
{
    public partial class GroupsPage : ContentPage
    {
        public GroupsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetGroupsAsync();
        }

        async void OnGroupAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroupEntryPage(new Group())
            {
                
                BindingContext = new Group()
            });
        }


        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new GroupEntryPage(e.SelectedItem as Group)
                {
                    
                    BindingContext = e.SelectedItem as Group
                });
            }
        }

    }
}