using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace VMT
{
    public partial class GroupEntryPage : ContentPage
    {
        Group _selectedGroup;
        ObservableCollection<Word> _words;
        public GroupEntryPage(Group group)
        {
            InitializeComponent();
            _selectedGroup = group;
            

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _words = VocabularySerializer.Deserialize(_selectedGroup.Words);
            wordslListView.ItemsSource = _words;

        }


        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var group = (Group)BindingContext;
            group.Words = VocabularySerializer.Serialize(_words);
            group.Size = _words.Count;
            await App.Database.SaveGroupAsync(group);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var group = (Group)BindingContext;
            bool answer = await DisplayAlert("", "Delete group?", "Yes", "No");
            if (answer)
            {
                await App.Database.DeleteGroupAsync(group);
                await Navigation.PopAsync();
            }
        }

        async void OnAddWordButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WordEntryPage(_selectedGroup));

        }

        async void OnShowDetailsButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Details", $"id: {_selectedGroup.ID}, name: {_selectedGroup.Name} , words: {_selectedGroup.Words}", "OK");

        }

        void OnDeleteWordClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var wordToDelete = menuItem.CommandParameter as Word;
            foreach (Word word in _words.ToList())
            {
                if (word.KnownWord == wordToDelete.KnownWord && word.LearntWord == wordToDelete.LearntWord)
                    _words.Remove(word);
            }

        }

        

    }
}