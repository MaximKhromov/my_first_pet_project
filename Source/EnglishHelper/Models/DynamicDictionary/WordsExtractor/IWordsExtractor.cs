using System.Collections.Generic;

namespace EnglishHelper.Models.DynamicDictionary.WordsExtractor
{
    /// <summary>
    /// Интерфейс экстактора слов из хранилища.
    /// </summary>
    public interface IWordsExtractor
    {
        /// <summary>
        /// Извлекает все слова из словаря.
        /// </summary>
        /// <returns>Коллекция слов из словаря.</returns>
        IEnumerable<DyctionaryItem> Extract();
    }
}