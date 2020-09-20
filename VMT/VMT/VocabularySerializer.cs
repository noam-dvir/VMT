using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VMT
{
    class VocabularySerializer
	{
		//convert string to list - deserilize
		//convert list to string serialize

		//hello$hola#four$quatro#cake$bolo
		public static ObservableCollection<Word> Deserialize(string stringData)
		{
			var result = new ObservableCollection<Word>();
			if (!String.IsNullOrEmpty(stringData))
			{
				string[] wordsArray = stringData.Split('#');
				foreach (string wordTuple in wordsArray)
					result.Add(new Word(
						wordTuple.Split('$')[0],
						wordTuple.Split('$')[1]
						));
			}			
			return result;
		}

		public static string Serialize(ObservableCollection<Word> wordsList)
		{
			StringBuilder sb = new StringBuilder();
			if (wordsList.Count > 0)
			{
				foreach (Word word in wordsList)
				{
					sb.Append(word.KnownWord)
						.Append('$')
						.Append(word.LearntWord)
						.Append('#');
				}
				sb.Length--; //remove last '#'
			}
			return sb.ToString();
		}

		/*

		public static List<Word> Deserialize(string stringData)
		{
			var result = new List<Word>();
			string[] wordsArray = stringData.Split('#');
			foreach (string wordTuple in wordsArray)
				result.Add(new Word(
					wordTuple.Split('$')[0],
					wordTuple.Split('$')[1]
					));
			return result;
		}
		*/

	}
}
