using System;
using System.Globalization;
using System.Text;

namespace LanguageGame
{
    public static class Translator
    {
        /// <summary>
        /// Translates from English to Pig Latin. Pig Latin obeys a few simple following rules:
        /// - if word starts with vowel sounds, the vowel is left alone, and most commonly 'yay' is added to the end;
        /// - if word starts with consonant sounds or consonant clusters, all letters before the initial vowel are
        ///   placed at the end of the word sequence. Then, "ay" is added.
        /// Note: If a word begins with a capital letter, then its translation also begins with a capital letter,
        /// if it starts with a lowercase letter, then its translation will also begin with a lowercase letter.
        /// </summary>
        /// <param name="phrase">Source phrase.</param>
        /// <returns>Phrase in Pig Latin.</returns>
        /// <exception cref="ArgumentException">Thrown if phrase is null or empty.</exception>
        /// <example>
        /// "apple" -> "appleyay"
        /// "Eat" -> "Eatyay"
        /// "explain" -> "explainyay"
        /// "Smile" -> "Ilesmay"
        /// "Glove" -> "Oveglay".
        /// </example>
        public static string TranslateToPigLatin(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                throw new ArgumentException("Source string cannot be null or empty or whitespace.", nameof(phrase));
            }

            var result = new StringBuilder();
            string[] wordsArr = phrase.Split(' ');
            for (int i = 0; i < wordsArr.Length; i++)
            {
                if (i == 0)
                {
                    result.Append(WordToPigLatin(wordsArr[i]));
                }
                else
                {
                    result.Append(' ' + WordToPigLatin(wordsArr[i]));
                }
            }

            return result.ToString();
        }

        private static string WordToPigLatin(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return string.Empty;
            }

            if (!char.IsLetter(phrase[0]))
            {
                return phrase;
            }

            if (phrase.IndexOf('-') != -1)
            {
                var recursion = new StringBuilder();
                string[] recursionArr = phrase.Split('-');
                foreach (string str in recursionArr)
                {
                    recursion.Append(WordToPigLatin(str) + '-');
                }

                return recursion.ToString().Remove(recursion.Length - 1);
            }

            int removeCheker = 0;
            char lustSymbol = '#';
            if (!char.IsLetter(phrase[phrase.Length - 1]))
            {
                removeCheker++;
                lustSymbol = phrase[phrase.Length - 1];
                phrase = phrase.Remove(phrase.Length - 1);
            }

            int firstLetter = (int)phrase[0];
            var result = new StringBuilder();
            int caunter = 0;

            if (IsVowel((char)firstLetter))
            {
                result.Append(phrase + "yay");
            }
            else if (firstLetter < 91 & firstLetter > 64)
            {
                while (!IsVowel(phrase[caunter]))
                {
                    caunter++;
                }

                result.Append(char.ToUpper(phrase[caunter], CultureInfo.InvariantCulture) + phrase.Substring(caunter + 1) + phrase.Substring(0, caunter).ToLowerInvariant() + "ay");
            }
            else
            {
                caunter = 0;
                while (!IsVowel(phrase[caunter]) & caunter < (phrase.Length - 1))
                {
                    caunter++;
                }

                result.Append(phrase.Substring(caunter) + phrase.Substring(0, caunter) + "ay");
            }

            if (removeCheker == 1)
            {
                return result.Append(lustSymbol).ToString();
            }

            return result.ToString();
        }

        private static bool IsVowel(char letter) => letter switch
        {
            'A' => true,
            'a' => true,
            'E' => true,
            'e' => true,
            'I' => true,
            'i' => true,
            'O' => true,
            'o' => true,
            'U' => true,
            'u' => true,
            _ => false,
        };
    }
}
