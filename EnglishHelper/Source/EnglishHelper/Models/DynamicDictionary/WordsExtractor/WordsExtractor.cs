using System.Collections.Generic;

namespace EnglishHelper.Models.DynamicDictionary.WordsExtractor
{
    /// <summary>
    /// Экстрактора слов из хранилища.
    /// </summary>
    public class WordsExtractor : IWordsExtractor
    {
        /// <inheritdoc />
        public IEnumerable<DyctionaryItem> Extract()
        {
            return new List<DyctionaryItem>
            {
                new DyctionaryItem
                {
                    Word = "an apple", Translate = @"яблоко"
                },

                new DyctionaryItem
                {
                    Word = @"a table", Translate = "стол"
                }
            };
        }
    }
}