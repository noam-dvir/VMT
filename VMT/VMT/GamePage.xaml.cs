using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VMT
{
    public class GameVars : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Group _group { get; set; }
        public List<Word> _words { get; set; }
        public Word _currentWord { get; set; }
        public List<Word> _currentDeck { get; set; }
        public List<Word> _incorrectWords { get; set; }
        public int _currentIndex { get; set; }
        public bool _displayKnown { get; set; }
        private int _iterationWordsLeft;
        public int IterationWordsLeft
        {
            get { return _iterationWordsLeft; }
            set
            {
                if (_iterationWordsLeft == value)
                    return;

                _iterationWordsLeft = value;
                OnPropertyChanged();
            }
        }
        
        private string _displayText;
        public string DisplayText 
        {
            get { return _displayText; } 
            set
            {
                if (_displayText == value)
                    return;

                _displayText = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage 
    {

        GameVars gameVars;
        public GamePage(Group group)
        {
            InitializeComponent();

            InitializeGameVariables(group);

        }

        

        private async void RightButtonClicked(object sender, EventArgs e)
        {
            if (!gameVars._displayKnown)
            {
                gameVars.IterationWordsLeft--;
                gameVars._currentIndex++;
                if(gameVars._currentIndex >= gameVars._currentDeck.Count) //finished iteration
                {
                    gameVars._currentDeck = WordShuffler.ShuffleDeck(gameVars._incorrectWords);
                    gameVars._currentIndex = 0;
                    gameVars.IterationWordsLeft = gameVars._currentDeck.Count;
                    gameVars._incorrectWords = new List<Word>();
                }

                if (IsGameOver()) //check if game over
                {
                    await DisplayAlert("Finish", "Well Done!", "Back");
                    await Navigation.PopAsync();
                }
                else  //new iteration, game not over
                {
                    
                    gameVars._displayKnown = true;
                    gameVars._currentWord = gameVars._currentDeck[gameVars._currentIndex];
                    gameVars.DisplayText = gameVars._currentWord.KnownWord;
                    if (gameVars._currentIndex == 0)
                        await DisplayAlert("", $"Starting new iteration with {gameVars._currentDeck.Count} words.", "OK");

                }

                FlipButtonsOpacity();
            }

        }


        private async void WrongButtonClicked(object sender, EventArgs e)
        {
            if (!gameVars._displayKnown)
            {
                gameVars._incorrectWords.Add(gameVars._currentWord);
                gameVars.IterationWordsLeft--;
                gameVars._currentIndex++;
                if (gameVars._currentIndex >= gameVars._currentDeck.Count) //finished iteration
                {
                    gameVars._currentDeck = WordShuffler.ShuffleDeck(gameVars._incorrectWords);
                    gameVars._currentIndex = 0;
                    gameVars.IterationWordsLeft = gameVars._currentDeck.Count;
                    gameVars._incorrectWords = new List<Word>();
                    await DisplayAlert("", $"Starting new iteration with {gameVars._currentDeck.Count} words.", "OK");
                }

                gameVars._displayKnown = true;
                gameVars._currentWord = gameVars._currentDeck[gameVars._currentIndex];
                gameVars.DisplayText = gameVars._currentWord.KnownWord;

                FlipButtonsOpacity();
            }
        }

        private void WordClicked(object sender, EventArgs e)
        {
            if (gameVars._displayKnown)
            {
                FlipButtonsOpacity();
                gameVars._displayKnown = false;
                gameVars.DisplayText = gameVars._currentWord.LearntWord;

            }

        }

        private bool IsGameOver()
        {
            return gameVars._currentDeck.Count == 0;
        }

        private void InitializeGameVariables(Group group)
        {
            gameVars = new GameVars();

            gameVars._group = group;
            gameVars._words = new List<Word>(VocabularySerializer.Deserialize(group.Words));
            gameVars._incorrectWords = new List<Word>();
            gameVars._currentIndex = 0;
            gameVars._currentDeck = WordShuffler.ShuffleDeck(gameVars._words);
            gameVars.IterationWordsLeft = gameVars._currentDeck.Count;
            gameVars._displayKnown = true;
            gameVars._currentWord = gameVars._currentDeck[gameVars._currentIndex];
            gameVars.DisplayText = gameVars._currentWord.KnownWord;

            Display.BindingContext = this.gameVars as GameVars;
            WordsLeft.BindingContext = this.gameVars as GameVars;

            FlipButtonsOpacity();
        }

        private void FlipButtonsOpacity()
        {
            RightButton.Opacity = (RightButton.Opacity == 0) ? 1 : 0;
            WrongButton.Opacity = (WrongButton.Opacity == 0) ? 1 : 0;
        }
    }
}