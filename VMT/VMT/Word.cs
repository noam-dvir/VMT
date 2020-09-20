using System;
using System.Collections.Generic;
using System.Text;

namespace VMT
{
	public class Word
	{

		public string KnownWord { get; set; }
		public string LearntWord { get; set; }

		public Word(string _known, string _learnt)
		{
			this.KnownWord = _known;
			this.LearntWord = _learnt;
		}
	}
}
