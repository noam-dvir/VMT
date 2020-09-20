using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VMT
{
    public class WordShuffler
    {
        public static List<Word> ShuffleDeck(List<Word> words)
        {
            int size = words.Count;

            var result = new List<Word>();
            //var randomIndixes = new int[size];
            var randomIndices = GetGetRandomPermutation(size);
            for (int i = 0; i < size; i++)
            {
                result.Add(words[randomIndices[i]]);
            }
            return result;
        }

        /*
         * return array of random pemutation of indexes
         * e.g GetGetRandomPermutation(7) = [6,3,5,0,4,1,2]
         */
        private static int[] GetGetRandomPermutation(int n)
        {
            var result = new int[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = i;
            }
            return ShuffleInts(result);

        }

        public static int[] ShuffleInts(int[] array)
        {
            Random random = new Random();
            int n = array.Count();
            while (n > 1)
            {
                n--;
                int i = random.Next(n + 1);
                int temp = array[i];
                array[i] = array[n];
                array[n] = temp;
            }
            return array;
        }
    }
}
