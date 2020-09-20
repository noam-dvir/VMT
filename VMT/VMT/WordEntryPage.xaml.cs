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
    public partial class WordEntryPage : ContentPage
    {
        Group _group;
        string _known;
        string _learnt;


        public WordEntryPage(Group group)
        {
            InitializeComponent();
            _group = group;
        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            //await DisplayAlert("Warning", $"{_group.Words}", "OK");

            //listView.ItemsSource = await App.Database.GetGroupsAsync();
        }


        async void SaveNewWordButtonClicked(object sender, EventArgs e)
        {
            AddWordToList(_group, new Word(_known, _learnt));
            
            await Navigation.PopAsync();
        }

        void OnLearntEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = e.NewTextValue;
            _learnt = newText; 
        }

        void OnKnownEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = e.NewTextValue;
            _known = newText;
        }

        public static void AddWordToList(Group g, Word newWord)
        {
            var wordsList = VocabularySerializer.Deserialize(g.Words);
            wordsList.Add(newWord);
            g.Words = VocabularySerializer.Serialize(wordsList);
        }
    }
}