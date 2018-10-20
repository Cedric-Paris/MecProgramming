using System;
using System.Collections.Generic;
using System.Linq;

namespace MecProgramming.Tools
{
    public class Autocomplete
    {
        /// <summary>
        /// Dictionary of the words in their context
        /// A key in the dictionary, give the frequency of all the words that can come after
        /// </summary>
        private Dictionary<string, Dictionary<string, int>> numberWordsContext = new Dictionary<string, Dictionary<string, int>>();

        /// <summary>
        /// Dictionary of the frequency of the words
        /// Count the number of apperance of each word
        /// </summary>
        private Dictionary<string, int> numberWords = new Dictionary<string, int>();

        /// <summary>
        /// Delimiters for the words
        /// </summary>
        private readonly char[] tokenDelimeters = { ' ', '.', '!', ',', '?', ';', ')', '(', ':', '"' };

        /// <summary>
        /// Delimiters for the sentenes to learn on 
        /// </summary>
        private readonly char[] sentenceDelimeters = { '.', '!', '\n', '?' };

        /// <summary>
        /// Constructor of the object
        /// Load the file to learn on all the sentences of this textfile
        /// </summary>
        /// <param name="filepath">Path of the textfile to learn on</param>
        public Autocomplete(string filepath)
        {
            string text = System.IO.File.ReadAllText(filepath);

            string[] sentences = text.Split(sentenceDelimeters, StringSplitOptions.RemoveEmptyEntries);

            // Train on each sentence
            foreach (string sentence in sentences)
            {
                train(sentence);
            }
        }

        /// <summary>
        /// Train the prediction on a sentence
        /// </summary>
        /// <param name="s">Sentence to learn on</param>
        public void train(string s)
        {
            // We ignore the uppercase during the prediction
            string input = s.ToLower();

            // All the words of the sentence
            string[] tokens = input.Split(tokenDelimeters, StringSplitOptions.RemoveEmptyEntries);

            string previousToken = "";

            /*
             * We count the occurences of each token (word) in their context 
             * We also count the occurences of each token in all their occurences
             */
            foreach (string token in tokens)
            {
                int valueContext = 0;
                int valueCount = 0;

                if (numberWordsContext.ContainsKey(previousToken) && numberWordsContext[previousToken].ContainsKey(token))
                    valueContext = numberWordsContext[previousToken][token];

                if (numberWords.ContainsKey(token))
                    valueCount = numberWords[token];

                if (!numberWordsContext.ContainsKey(previousToken))
                    numberWordsContext[previousToken] = new Dictionary<string, int>();

                // Increment the number of occurences
                numberWordsContext[previousToken][token] = valueContext + 1;
                numberWords[token] = valueCount + 1;

                previousToken = token;
            }
        }

        /// <summary>
        /// Make a prediction on a given sentence to output the most likely words to come next
        /// </summary>
        /// <param name="s">Sentence to use for the prediction</param>
        /// <param name="learn">Boolean that indicate if we want to train the model on this sentence too</param>
        /// <returns>List of the words that are the most likely to come next</returns>
        public List<String> predict(string s, bool learn = false)
        {
            // We ignore the uppercase during the prediction
            string input = s.ToLower();

            // If we want to train the model on this sentence
            if (learn)
                this.train(input);

            List<string> predicition = new List<string>();

            List<string> tokens = new List<string>();
            tokens.Insert(0, "");
            char lastChar = ' ';

            /*
             * If the input is empty
             * We just want to predict the most problable words to start a sentences
             */
            if (input.Length == 0)
            {
                predicition.Add("I");
                predicition.Add("Yes");
                predicition.Add("No");
                predicition.Add("You");
                return predicition;
            }
            else if (input.Length > 0)
            {
                tokens = input.Split(tokenDelimeters, StringSplitOptions.RemoveEmptyEntries).ToList();
                tokens.Insert(0, "");
                lastChar = input[input.Length - 1];
            }


            /* 
             * Words prediction
             * If the last character is a delimiter character ('.', ' ', ',')
             * It means that we want to predict a need word
             */
            if (tokenDelimeters.Contains(lastChar))
            {
                Dictionary<string, int> data = new Dictionary<string, int>();
                
                if(numberWordsContext.ContainsKey(tokens.Last()))
                    data = numberWordsContext[tokens.Last()];
                

                // We make a query in the frequency of word in the current context
                predicition = (from x in data
                               orderby x.Value descending
                               select x.Key).ToList();
            }
            /* 
             * Words completion
             * If we are writing a word, we want the possible completion of this word
             * The completion can be in the context of the previous word or just a word starting the same
             */
            else
            {
                Dictionary<string, int> data = new Dictionary<string, int>();

                if(numberWordsContext.ContainsKey(tokens[tokens.Count - 2]))
                    data = numberWordsContext[tokens[tokens.Count - 2]];

                string wordToComplete = tokens.Last();

                // We make a query in the frequency of word in the current context
                List<string> contextWordCompletion = (
                    from x in data
                    where (x.Key.Length >= wordToComplete.Length && x.Key.Substring(0, wordToComplete.Length) == wordToComplete)
                    orderby x.Value descending
                    select x.Key
                ).ToList();

                // We make a query in the frequency of word just starting with the same letter
                List<string> wordCompletion = (
                    from x in numberWords
                    where (x.Key.Length >= wordToComplete.Length && x.Key.Substring(0, wordToComplete.Length) == wordToComplete)
                    orderby x.Value descending
                    select x.Key
                ).ToList();

                predicition = contextWordCompletion;
                predicition.AddRange(wordCompletion);

                // We had the currently typing word to the possible completions
                predicition.Add(wordToComplete);
            }

            return predicition;
        }
    }
}
